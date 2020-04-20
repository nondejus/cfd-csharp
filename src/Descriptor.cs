using System;
using System.Numerics;

/// <summary>
/// cfd library namespace.
/// </summary>
namespace Cfd
{
  /**
   * @brief descriptor script type.
   */
  public enum CfdDescriptorScriptType
  {
    Null = 0,     //!< null
    Sh,           //!< script hash
    Wsh,          //!< segwit script hash
    Pk,           //!< pubkey
    Pkh,          //!< pubkey hash
    Wpkh,         //!< segwit pubkey hash
    Combo,        //!< combo
    Multi,        //!< multisig
    SortedMulti,  //!< sorted multisig
    Addr,         //!< address
    Raw           //!< raw script
  };

  /**
   * @brief descriptor key type.
   */
  public enum CfdDescriptorKeyType
  {
    Null = 0,  //!< null
    Public,    //!< pubkey
    Bip32,     //!< bip32 extpubkey
    Bip32Priv  //!< bip32 extprivkey
  };

  public struct CfdKeyData : IEquatable<CfdKeyData>
  {
    public CfdDescriptorKeyType KeyType { get; }
    public Pubkey Pubkey { get; }
    public ExtPubkey ExtPubkey { get; }
    public ExtPrivkey ExtPrivkey { get; }


    public CfdKeyData(Pubkey pubkey)
    {
      this.KeyType = CfdDescriptorKeyType.Public;
      this.Pubkey = pubkey;
      this.ExtPubkey = new ExtPubkey();
      this.ExtPrivkey = new ExtPrivkey();
    }

    public CfdKeyData(ExtPubkey extPubkey)
    {
      this.KeyType = CfdDescriptorKeyType.Bip32;
      this.Pubkey = new Pubkey();
      this.ExtPubkey = extPubkey;
      this.ExtPrivkey = new ExtPrivkey();
    }

    public CfdKeyData(ExtPrivkey extPrivkey)
    {
      this.KeyType = CfdDescriptorKeyType.Bip32Priv;
      this.Pubkey = new Pubkey();
      this.ExtPubkey = new ExtPubkey();
      this.ExtPrivkey = extPrivkey;
    }

    public CfdKeyData(CfdDescriptorKeyType keyType, Pubkey pubkey,
        ExtPubkey extPubkey, ExtPrivkey extPrivkey)
    {
      this.KeyType = keyType;
      this.Pubkey = pubkey;
      this.ExtPubkey = extPubkey;
      this.ExtPrivkey = extPrivkey;
    }

    public bool Equals(CfdKeyData other)
    {
      if (KeyType != other.KeyType)
      {
        return false;
      }
      else if (KeyType == CfdDescriptorKeyType.Bip32)
      {
        return ExtPubkey.Equals(other.ExtPubkey);
      }
      else if (KeyType == CfdDescriptorKeyType.Bip32Priv)
      {
        return ExtPrivkey.Equals(other.ExtPrivkey);
      }
      else if (KeyType == CfdDescriptorKeyType.Public)
      {
        return Pubkey.Equals(other.Pubkey);
      }
      else
      {
        return false;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj is null)
      {
        return false;
      }
      if (obj is CfdKeyData)
      {
        return this.Equals((CfdKeyData)obj);
      }
      return false;
    }

    public override int GetHashCode()
    {
      if (KeyType == CfdDescriptorKeyType.Bip32)
      {
        return KeyType.GetHashCode() + ExtPubkey.GetHashCode();
      }
      else if (KeyType == CfdDescriptorKeyType.Bip32Priv)
      {
        return KeyType.GetHashCode() + ExtPrivkey.GetHashCode();
      }
      else if (KeyType == CfdDescriptorKeyType.Public)
      {
        return KeyType.GetHashCode() + Pubkey.GetHashCode();
      }
      else
      {
        return KeyType.GetHashCode();
      }
    }

    public static bool operator ==(CfdKeyData left, CfdKeyData right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(CfdKeyData left, CfdKeyData right)
    {
      return !(left == right);
    }
  }

  public struct CfdDescriptorScriptData : IEquatable<CfdDescriptorScriptData>
  {
    public CfdDescriptorScriptType ScriptType { get; }
    public uint Depth { get; }
    public CfdHashType HashType { get; }
    public Address Address { get; }
    public Script RedeemScript { get; }
    public CfdKeyData KeyData { get; }
    public Vector<CfdKeyData> MultisigKeyList { get; }
    public uint MultisigRequireNum { get; }

    public CfdDescriptorScriptData(CfdDescriptorScriptType scriptType, uint depth,
        Script redeemScript)
    {
      this.ScriptType = scriptType;
      this.Depth = depth;
      this.HashType = CfdHashType.P2sh;
      this.Address = new Address();
      this.RedeemScript = redeemScript;
      this.KeyData = new CfdKeyData();
      this.MultisigKeyList = new Vector<CfdKeyData>();
      this.MultisigRequireNum = 0;
    }

    public CfdDescriptorScriptData(CfdDescriptorScriptType scriptType, uint depth,
        CfdHashType hashType, Address address)
    {
      this.ScriptType = scriptType;
      this.Depth = depth;
      this.HashType = hashType;
      this.Address = address;
      this.RedeemScript = new Script();
      this.KeyData = new CfdKeyData();
      this.MultisigKeyList = new Vector<CfdKeyData>();
      this.MultisigRequireNum = 0;
    }

    public CfdDescriptorScriptData(CfdDescriptorScriptType scriptType, uint depth,
        CfdHashType hashType, Address address, CfdKeyData keyData)
    {
      this.ScriptType = scriptType;
      this.Depth = depth;
      this.HashType = hashType;
      this.Address = address;
      this.RedeemScript = new Script();
      this.KeyData = keyData;
      this.MultisigKeyList = new Vector<CfdKeyData>();
      this.MultisigRequireNum = 0;
    }

    public CfdDescriptorScriptData(CfdDescriptorScriptType scriptType, uint depth,
    CfdHashType hashType, Address address, Script redeemScript)
    {
      this.ScriptType = scriptType;
      this.Depth = depth;
      this.HashType = hashType;
      this.Address = address;
      this.RedeemScript = redeemScript;
      this.KeyData = new CfdKeyData();
      this.MultisigKeyList = new Vector<CfdKeyData>();
      this.MultisigRequireNum = 0;
    }

    public CfdDescriptorScriptData(CfdDescriptorScriptType scriptType, uint depth,
        CfdHashType hashType, Address address, Script redeemScript,
        CfdKeyData[] multisigKeyList, uint multisigRequireNum)
    {
      this.ScriptType = scriptType;
      this.Depth = depth;
      this.HashType = hashType;
      this.Address = address;
      this.RedeemScript = redeemScript;
      this.KeyData = new CfdKeyData();
      this.MultisigKeyList = new Vector<CfdKeyData>(multisigKeyList);
      this.MultisigRequireNum = multisigRequireNum;
    }

    public CfdDescriptorScriptData(CfdDescriptorScriptType scriptType, uint depth,
        CfdHashType hashType, Address address, Script redeemScript,
        CfdKeyData keyData, CfdKeyData[] multisigKeyList, uint multisigRequireNum)
    {
      this.ScriptType = scriptType;
      this.Depth = depth;
      this.HashType = hashType;
      this.Address = address;
      this.RedeemScript = redeemScript;
      this.KeyData = keyData;
      this.MultisigKeyList = new Vector<CfdKeyData>(multisigKeyList);
      this.MultisigRequireNum = multisigRequireNum;
    }

    public bool Equals(CfdDescriptorScriptData other)
    {
      if (ScriptType != other.ScriptType)
      {
        return false;
      }
      switch (ScriptType)
      {
        case CfdDescriptorScriptType.Pk:
        case CfdDescriptorScriptType.Pkh:
        case CfdDescriptorScriptType.Wpkh:
        case CfdDescriptorScriptType.Combo:
          return KeyData.Equals(other.KeyData);
        case CfdDescriptorScriptType.Sh:
        case CfdDescriptorScriptType.Wsh:
        case CfdDescriptorScriptType.Multi:
        case CfdDescriptorScriptType.SortedMulti:
        case CfdDescriptorScriptType.Raw:
          return RedeemScript.Equals(other.RedeemScript);
        case CfdDescriptorScriptType.Addr:
          return Address.Equals(other.Address);
        default:
          return false;
      }
    }

    public override bool Equals(object obj)
    {
      if (obj is null)
      {
        return false;
      }
      if (obj is CfdDescriptorScriptData)
      {
        return this.Equals((CfdDescriptorScriptData)obj);
      }
      return false;
    }

    public override int GetHashCode()
    {
      switch (ScriptType)
      {
        case CfdDescriptorScriptType.Pk:
        case CfdDescriptorScriptType.Pkh:
        case CfdDescriptorScriptType.Wpkh:
        case CfdDescriptorScriptType.Combo:
          return ScriptType.GetHashCode() + KeyData.GetHashCode();
        case CfdDescriptorScriptType.Sh:
        case CfdDescriptorScriptType.Wsh:
        case CfdDescriptorScriptType.Multi:
        case CfdDescriptorScriptType.SortedMulti:
        case CfdDescriptorScriptType.Raw:
          return ScriptType.GetHashCode() + RedeemScript.GetHashCode();
        case CfdDescriptorScriptType.Addr:
          return ScriptType.GetHashCode() + Address.GetHashCode();
        default:
          return ScriptType.GetHashCode();
      }
    }

    public static bool operator ==(CfdDescriptorScriptData left, CfdDescriptorScriptData right)
    {
      return left.Equals(right);
    }

    public static bool operator !=(CfdDescriptorScriptData left, CfdDescriptorScriptData right)
    {
      return !(left == right);
    }
  }

  public class Descriptor
  {
    private readonly string descriptor;
    private Vector<CfdDescriptorScriptData> scriptList;

    public Descriptor(string descriptorString, CfdNetworkType network)
    {
      if (descriptorString is null)
      {
        throw new ArgumentNullException(nameof(descriptorString));
      }
      using (var handle = new ErrorHandle())
      {
        descriptor = GetDescriptorChecksum(handle, descriptorString, network);
        scriptList = ParseDescriptor(handle, descriptorString, "", network);
      }
    }

    public Descriptor(string descriptorString, string derivePath, CfdNetworkType network)
    {
      if (descriptorString is null)
      {
        throw new ArgumentNullException(nameof(descriptorString));
      }
      if (derivePath is null)
      {
        throw new ArgumentNullException(nameof(derivePath));
      }
      using (var handle = new ErrorHandle())
      {
        descriptor = GetDescriptorChecksum(handle, descriptorString, network);
        scriptList = ParseDescriptor(handle, descriptorString, derivePath, network);
      }
    }

    private static string GetDescriptorChecksum(ErrorHandle handle, string descriptorString,
        CfdNetworkType network)
    {
      var ret = NativeMethods.CfdGetDescriptorChecksum(handle.GetHandle(), (int)network,
          descriptorString, out IntPtr descriptorAddedChecksum);
      if (ret != CfdErrorCode.Success)
      {
        handle.ThrowError(ret);
      }
      return CCommon.ConvertToString(descriptorAddedChecksum);
    }

    private static Vector<CfdDescriptorScriptData> ParseDescriptor(
        ErrorHandle handle, string descriptorString,
        string derivePath, CfdNetworkType network)
    {
      var ret = NativeMethods.CfdParseDescriptor(
        handle.GetHandle(), descriptorString, (int)network, derivePath,
        out IntPtr descriptorHandle, out uint maxIndex);
      if (ret != CfdErrorCode.Success)
      {
        handle.ThrowError(ret);
      }
      try
      {
        bool isMultisig = false;
        uint maxKeyNum = 0;
        uint requireNum = 0;
        CfdDescriptorScriptData[] list = new CfdDescriptorScriptData[maxIndex];
        for (uint index = 0; index < maxIndex; ++index)
        {
          ret = NativeMethods.CfdGetDescriptorData(
            handle.GetHandle(), descriptorHandle, index, out _, out uint depth,
            out int scriptType, out IntPtr lockingScript, out IntPtr address,
            out int hashType, out IntPtr redeemScript,
            out int keyType, out IntPtr pubkey, out IntPtr extPubkey, out IntPtr extPrivkey,
            out isMultisig, out maxKeyNum, out requireNum);
          if (ret != CfdErrorCode.Success)
          {
            handle.ThrowError(ret);
          }
          string tempAddress = CCommon.ConvertToString(address);
          string tempRedeemScript = CCommon.ConvertToString(redeemScript);
          string tempPubkey = CCommon.ConvertToString(pubkey);
          string tempExtPubkey = CCommon.ConvertToString(extPubkey);
          string tempExtPrivkey = CCommon.ConvertToString(extPrivkey);
          CfdDescriptorScriptData data;
          CfdKeyData keyData;
          CfdDescriptorKeyType type;
          switch ((CfdDescriptorScriptType)scriptType)
          {
            case CfdDescriptorScriptType.Combo:
            case CfdDescriptorScriptType.Pk:
            case CfdDescriptorScriptType.Pkh:
            case CfdDescriptorScriptType.Wpkh:
              type = (CfdDescriptorKeyType)keyType;
              if (type == CfdDescriptorKeyType.Bip32)
              {
                keyData = new CfdKeyData(new ExtPubkey(tempExtPubkey));
              }
              else if (type == CfdDescriptorKeyType.Bip32Priv)
              {
                keyData = new CfdKeyData(new ExtPrivkey(tempExtPrivkey));
              }
              else
              {
                keyData = new CfdKeyData(new Pubkey(tempPubkey));
              }
              data = new CfdDescriptorScriptData(
                (CfdDescriptorScriptType)scriptType,
                depth, (CfdHashType)hashType, new Address(tempAddress), keyData);
              break;
            case CfdDescriptorScriptType.Sh:
            case CfdDescriptorScriptType.Wsh:
            case CfdDescriptorScriptType.Multi:
            case CfdDescriptorScriptType.SortedMulti:
              if (isMultisig)
              {
                CfdKeyData[] keyList = new CfdKeyData[maxKeyNum];
                for (uint multisigIndex = 0; multisigIndex < maxKeyNum; ++multisigIndex)
                {
                  ret = NativeMethods.CfdGetDescriptorMultisigKey(handle.GetHandle(), descriptorHandle,
                    multisigIndex, out int multisigKeyType, out IntPtr multisigPubkey,
                    out IntPtr multisigExtPubkey, out IntPtr multisigExtPrivkey);
                  if (ret != CfdErrorCode.Success)
                  {
                    handle.ThrowError(ret);
                  }
                  tempPubkey = CCommon.ConvertToString(multisigPubkey);
                  tempExtPubkey = CCommon.ConvertToString(multisigExtPubkey);
                  tempExtPrivkey = CCommon.ConvertToString(multisigExtPrivkey);
                  type = (CfdDescriptorKeyType)multisigKeyType;
                  if (type == CfdDescriptorKeyType.Bip32)
                  {
                    keyList[multisigIndex] = new CfdKeyData(new ExtPubkey(tempExtPubkey));
                  }
                  else if (type == CfdDescriptorKeyType.Bip32Priv)
                  {
                    keyList[multisigIndex] = new CfdKeyData(new ExtPrivkey(tempExtPrivkey));
                  }
                  else
                  {
                    keyList[multisigIndex] = new CfdKeyData(new Pubkey(tempPubkey));
                  }
                }
                data = new CfdDescriptorScriptData(
                    (CfdDescriptorScriptType)scriptType,
                    depth, (CfdHashType)hashType, new Address(tempAddress),
                    new Script(tempRedeemScript), keyList, requireNum);
              }
              else
              {
                data = new CfdDescriptorScriptData(
                  (CfdDescriptorScriptType)scriptType,
                  depth, (CfdHashType)hashType, new Address(tempAddress),
                  new Script(tempRedeemScript));
              }
              break;
            case CfdDescriptorScriptType.Raw:
              data = new CfdDescriptorScriptData(
                (CfdDescriptorScriptType)scriptType,
                depth, new Script(tempRedeemScript));
              break;
            case CfdDescriptorScriptType.Addr:
              data = new CfdDescriptorScriptData(
                (CfdDescriptorScriptType)scriptType,
                depth, (CfdHashType)hashType, new Address(tempAddress));
              break;
            default:
              data = new CfdDescriptorScriptData();
              break;
          }
          list[index] = data;
        }
        return new Vector<CfdDescriptorScriptData>(list);
      }
      finally
      {
        NativeMethods.CfdFreeDescriptorHandle(
          handle.GetHandle(), descriptorHandle);
      }
    }

    public override string ToString()
    {
      return descriptor;
    }

    public Vector<CfdDescriptorScriptData> GetList()
    {
      return scriptList;
    }
  }
}
