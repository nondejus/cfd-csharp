using System;

/// <summary>
/// cfd library namespace.
/// </summary>
namespace Cfd
{
  public static class ScriptUtil
  {
    public static Script CreateMultisig(Pubkey[] pubkeyList, uint requireNum)
    {
      if (pubkeyList is null)
      {
        throw new ArgumentNullException(nameof(pubkeyList));
      }
      if ((pubkeyList.Length == 0) || (pubkeyList.Length < requireNum))
      {
        CfdCommon.ThrowError(CfdErrorCode.IllegalArgumentError, "Failed to multisig value.");
      }
      using (var handle = new ErrorHandle())
      {
        var ret = NativeMethods.CfdInitializeMultisigScript(
            handle.GetHandle(),
            (int)CfdNetworkType.Mainnet,
            (int)CfdHashType.P2wsh,
            out IntPtr multisigHandle);
        if (ret != CfdErrorCode.Success)
        {
          handle.ThrowError(ret);
        }
        try
        {
          for (uint index = 0; index < pubkeyList.Length; ++index)
          {
            ret = NativeMethods.CfdAddMultisigScriptData(
                handle.GetHandle(), multisigHandle,
                pubkeyList[index].ToHexString());
            if (ret != CfdErrorCode.Success)
            {
              handle.ThrowError(ret);
            }
          }

          ret = NativeMethods.CfdFinalizeMultisigScript(
              handle.GetHandle(), multisigHandle, requireNum,
              out IntPtr address,
              out IntPtr redeemScript,
              out IntPtr witnessScript);
          if (ret != CfdErrorCode.Success)
          {
            handle.ThrowError(ret);
          }
          CCommon.ConvertToString(address);
          CCommon.ConvertToString(redeemScript);
          return new Script(CCommon.ConvertToString(witnessScript));
        }
        finally
        {
          NativeMethods.CfdFreeMultisigScriptHandle(handle.GetHandle(), multisigHandle);
        }
      }
    }
  }
}
