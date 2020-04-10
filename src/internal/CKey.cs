using System;
using System.Runtime.InteropServices;

namespace Cfd
{
  internal static class CKey
  {
    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCalculateEcSignature(
          [In] IntPtr handle,
          [In] string sighash,
          [In] string privkey,
          [In] string wif,
          [In] int networkType,
          [In] bool hasGrindR,
          [Out] out IntPtr signature);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdEncodeSignatureByDer(
          [In] IntPtr handle,
          [In] string signature,
          [In] int sighashType,
          [In] bool sighashAnyoneCanPay,
          [Out] out IntPtr derSignature);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdDecodeSignatureFromDer(
          [In] IntPtr handle,
          [In] string derSignature,
          [Out] out IntPtr signature,
          [Out] out int sighashType,
          [Out] out bool sighashAnyoneCanPay);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdNormalizeSignature(
          [In] IntPtr handle,
          [In] string signature,
          [Out] out IntPtr normalizedSignature);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCreateKeyPair(
          [In] IntPtr handle,
          [In] bool isCompressed,
          [In] int networkType,
          [Out] out IntPtr pubkey,
          [Out] out IntPtr privkey,
          [Out] out IntPtr wif);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetPrivkeyFromWif(
          [In] IntPtr handle,
          [In] string wif,
          [In] int networkType,
          [Out] out IntPtr privkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetPrivkeyWif(
          [In] IntPtr handle,
          [In] string privkey,
          [In] int networkType,
          [In] bool isCompressed,
          [Out] out IntPtr wif);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdParsePrivkeyWif(
          [In] IntPtr handle,
          [In] string wif,
          [Out] out IntPtr privkey,
          [Out] out int networkType,
          [Out] out bool isCompressed);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetPubkeyFromPrivkey(
          [In] IntPtr handle,
          [In] string privkey,
          [In] string wif,
          [In] bool isCompressed,
          [Out] out IntPtr pubkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCompressPubkey(
          [In] IntPtr handle,
          [In] string pubkey,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdUncompressPubkey(
          [In] IntPtr handle,
          [In] string pubkey,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdInitializeCombinePubkey(
          [In] IntPtr handle,
          [Out] out IntPtr combineHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddCombinePubkey(
          [In] IntPtr handle,
          [In] IntPtr combineHandle,
          [In] string pubkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFinalizeCombinePubkey(
          [In] IntPtr handle,
          [In] IntPtr combineHandle,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFreeCombinePubkeyHandle(
          [In] IntPtr handle,
          [In] IntPtr combineHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdPubkeyTweakAdd(
          [In] IntPtr handle,
          [In] string pubkey,
          [In] string tweak,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdPubkeyTweakMul(
          [In] IntPtr handle,
          [In] string pubkey,
          [In] string tweak,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdNegatePubkey(
          [In] IntPtr handle,
          [In] string pubkey,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdPrivkeyTweakAdd(
          [In] IntPtr handle,
          [In] string privkey,
          [In] string tweak,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdPrivkeyTweakMul(
          [In] IntPtr handle,
          [In] string privkey,
          [In] string tweak,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdNegatePrivkey(
          [In] IntPtr handle,
          [In] string privkey,
          [Out] out IntPtr output);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCreateExtkeyFromSeed(
          [In] IntPtr handle,
          [In] string seedHex,
          [In] int networkType,
          [In] int keyType,
          [Out] out IntPtr extkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCreateExtkey(
          [In] IntPtr handle,
          [In] int keyType,
          [In] string parentKey,
          [In] string fingerprint,
          [In] string key,
          [In] string chainCode,
          [In] byte depth,
          [In] uint childNumber,
          [Out] out IntPtr extkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCreateExtkeyFromParent(
          [In] IntPtr handle,
          [In] string extkey,
          [In] uint childNumber,
          [In] bool hardened,
          [In] int networkType,
          [In] int keyType,
          [Out] out IntPtr childExtkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCreateExtkeyFromParentPath(
          [In] IntPtr handle,
          [In] string extkey,
          [In] string path,
          [In] int networkType,
          [In] int keyType,
          [Out] out IntPtr childExtkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCreateExtPubkey(
          [In] IntPtr handle,
          [In] string extkey,
          [In] int networkType,
          [Out] out IntPtr extPubkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetPrivkeyFromExtkey(
          [In] IntPtr handle,
          [In] string extkey,
          [In] int networkType,
          [Out] out IntPtr privkey,
          [Out] out IntPtr wif);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetPubkeyFromExtkey(
          [In] IntPtr handle,
          [In] string extkey,
          [In] int networkType,
          [Out] out IntPtr pubkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetParentExtkeyPathData(
          [In] IntPtr handle,
          [In] string parentExtkey,
          [In] string path,
          [In] int childKeyType,
          [Out] out IntPtr keyPathData,
          [Out] out IntPtr childKey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetExtkeyInformation(
          [In] IntPtr handle,
          [In] string extkey,
          [Out] out IntPtr version,
          [Out] out IntPtr fingerprint,
          [Out] out IntPtr chainCode,
          [Out] out uint depth,
          [Out] out uint childNumber);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdInitializeMnemonicWordList(
          [In] IntPtr handle,
          [In] string language,
          [Out] out IntPtr mnemonicHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetMnemonicWord(
          [In] IntPtr handle,
          [In] IntPtr mnemonicHandle,
          [Out] out IntPtr mnemonicWord);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFreeMnemonicWordList(
          [In] IntPtr handle,
          [In] IntPtr mnemonicHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode ConvertMnemonicToSeed(
          [In] IntPtr handle,
          [In] string mnemonic,
          [In] string passphrase,
          [In] bool strictCheck,
          [In] string language,
          [In] bool useIdeographicSpace,
          [Out] out IntPtr seed,
          [Out] out IntPtr entropy);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode ConvertEntropyToMnemonic(
          [In] IntPtr handle,
          [In] string entropy,
          [In] string language,
          [Out] out IntPtr mnemonic);
  }
}
