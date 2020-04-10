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
    internal static extern CfdErrorCode CfdNormalizeSignature(
        void* handle, const char* signature, char** normalizedSignature);

    internal static extern CfdErrorCode CfdCreateKeyPair(
        void* handle, bool isCompressed, int networkType, char** pubkey,
        char** privkey, char** wif);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetPrivkeyFromWif(
          [In] IntPtr handle,
          [In] string wif,
          [In] int networkType,
          [Out] out IntPtr privkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdParsePrivkeyWif(
          [In] IntPtr handle,
          [In] string wif,
          [Out] out IntPtr privkey,
          [Out] out int networkType,
          [Out] out bool isCompressed);
    CFDC_API int CfdGetPrivkeyWif(
        void* handle, const char* privkey, int network_type, bool is_compressed,
    char** wif);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetPubkeyFromPrivkey(
          [In] IntPtr handle,
          [In] string privkey,
          [In] string wif,
          [In] bool isCompressed,
          [Out] out IntPtr pubkey);


    internal static extern CfdErrorCode CfdCreateExtkeyFromSeed(
        void* handle, const char* seedHex, int networkType, int keyType,
            char** extkey);
    CFDC_API int CfdCreateExtkey(
        void* handle, int key_type, const char* parent_key,
    const char* fingerprint, const char* key, const char* chain_code,
    unsigned char depth, uint32_t child_num, char** extkey);
    CFDC_API int CfdCreateExtkeyFromParent(
        void* handle, const char* extkey, uint32_t child_number, bool hardened,
    int network_type, int key_type, char** child_extkey);
    internal static extern CfdErrorCode CfdCreateExtkeyFromParentPath(
        void* handle, const char* extkey, const char* path, int networkType,
            int keyType, char** childExtkey);

    internal static extern CfdErrorCode CfdCreateExtPubkey(
        void* handle, const char* extkey, int networkType, char** extPubkey);

    internal static extern CfdErrorCode CfdGetPrivkeyFromExtkey(
        void* handle, const char* extkey, int networkType, char** privkey,
            char** wif);

    internal static extern CfdErrorCode CfdGetPubkeyFromExtkey(
        void* handle, const char* extkey, int networkType, char** pubkey);

    CFDC_API int CfdGetParentExtkeyPathData(
        void* handle, const char* parent_extkey, const char* path,
    int child_key_type, char** key_path_data, char** child_key);

    CFDC_API int CfdGetExtkeyInformation(
        void* handle, const char* extkey, char** version, char** fingerprint,
    char** chain_code, uint32_t* depth, uint32_t* child_number);

    CFD_CAPI int CfdInitializeMnemonicWordList(
    void* handle, const char* language, void** mnemonic_handle);

    CFD_CAPI int CfdGetMnemonicWord(
    void* handle, void* mnemonic_handle, char** mnemonic_word);

    CFD_CAPI int CfdFreeMnemonicWordList(
    void* handle, void* mnemonic_handle);



    CFD_CAPI int ConvertMnemonicToSeed(
    void* handle, const char* mnemonic_word, const char* passphrase,
    bool strict_check, const char* language, bool use_ideographic_space,
    char** seed, char** entropy);


    CFD_CAPI int ConvertEntropyToMnemonic(
        void* handle, const char* entropy, const char* language,
    char** mnemonic_word);

  }
}
