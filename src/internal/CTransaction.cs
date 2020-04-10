using System;
using System.Runtime.InteropServices;

namespace Cfd
{
  /// <summary>
  /// cfd library (transaction) access class.
  /// </summary>
  internal static class CTransaction
  {
    CFDC_API int CfdInitializeTransaction(
    void* handle, int net_type, uint32_t version, uint32_t locktime,
    const char* tx_hex_string, void** create_handle);


    CFDC_API int CfdAddTransactionInput(
    void* handle, void* create_handle, const char* txid, uint32_t vout,
    uint32_t sequence);

    CFDC_API int CfdAddTransactionOutput(
    void* handle, void* create_handle, int64_t value_satoshi,
    const char* address, const char* direct_locking_script,
    const char* asset_string);

    CFDC_API int CfdFinalizeTransaction(
    void* handle, void* create_handle, char** tx_hex_string);

    CFDC_API int CfdFreeTransactionHandle(void* handle, void* create_handle);









    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddTxSign(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] string txid,
        [In] uint vout,
        [In] int hashType,
        [In] string signDataHex,
        [In] bool useDerEncode,
        [In] int sighashType,
        [In] bool sighashAnyoneCanPay,
        [In] bool clearStack,
        [Out] out IntPtr txString);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddPubkeyHashSign(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] string txid,
        [In] uint vout,
        [In] int hashType,
        [In] string pubkey,
        [In] string signature,
        [In] bool useDerEncode,
        [In] int sighashType,
        [In] bool sighashAnyoneCanPay,
        [Out] out IntPtr txString);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddScriptHashSign(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] string txid,
        [In] uint vout,
        [In] int hashType,
        [In] string redeemScript,
        [In] bool clearStack,
        [Out] out IntPtr txString);

    CFDC_API int CfdAddSignWithPrivkeySimple(
    void* handle, int net_type, const char* tx_hex_string, const char* txid,
    uint32_t vout, int hash_type, const char* pubkey, const char* privkey,
    int64_t value_satoshi, int sighash_type, bool sighash_anyone_can_pay,
    bool has_grind_r, char** tx_string);


    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdInitializeMultisigSign(
        [In] IntPtr handle,
        [Out] out IntPtr multiSignHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddMultisigSignData(
        [In] IntPtr handle,
        [In] IntPtr multiSignHandle,
        [In] string signature,
        [In] string relatedPubkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddMultisigSignDataToDer(
        [In] IntPtr handle,
        [In] IntPtr multiSignHandle,
        [In] string signature,
        [In] int sighashType,
        [In] bool sighashAnyoneCanPay,
        [In] string relatedPubkey);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFinalizeMultisigSign(
        [In] IntPtr handle,
        [In] IntPtr multiSignHandle,
        [In] int networkType,
        [In] string txHexString,
        [In] string txid,
        [In] uint vout,
        [In] int hashType,
        [In] string redeemScript,
        [Out] out IntPtr txString);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFreeMultisigSignHandle(
        [In] IntPtr handle,
        [In] IntPtr multiSignHandle);


    CFDC_API int CfdCreateSighash(
    void* handle, int net_type, const char* tx_hex_string, const char* txid,
    uint32_t vout, int hash_type, const char* pubkey,
    const char* redeem_script, int64_t value_satoshi, int sighash_type,
    bool sighash_anyone_can_pay, char** sighash);


    CFDC_API int CfdGetTxInfo(
    void* handle, int net_type, const char* tx_hex_string, char** txid,
    char** wtxid, uint32_t* size, uint32_t* vsize, uint32_t* weight,
    uint32_t* version, uint32_t* locktime);


      CFDC_API int CfdGetTxIn(
    void* handle, int net_type, const char* tx_hex_string, uint32_t index,
    char** txid, uint32_t* vout, uint32_t* sequence, char** script_sig);

    CFDC_API int CfdGetTxInWitness(
    void* handle, int net_type, const char* tx_hex_string, uint32_t txin_index,
    uint32_t stack_index, char** stack_data);

    CFDC_API int CfdGetTxOut(
    void* handle, int net_type, const char* tx_hex_string, uint32_t index,
    int64_t* value_satoshi, char** locking_script);

    CFDC_API int CfdGetTxInCount(
    void* handle, int net_type, const char* tx_hex_string, uint32_t* count);

      CFDC_API int CfdGetTxInWitnessCount(
    void* handle, int net_type, const char* tx_hex_string, uint32_t txin_index,
    uint32_t* count);
    CFDC_API int CfdGetTxOutCount(
        void* handle, int net_type, const char* tx_hex_string, uint32_t* count);
CFDC_API int CfdGetTxInIndex(
    void* handle, int net_type, const char* tx_hex_string, const char* txid,
    uint32_t vout, uint32_t* index);
    CFDC_API int CfdGetTxOutIndex(
        void* handle, int net_type, const char* tx_hex_string, const char* address,
    const char* direct_locking_script, uint32_t* index);

  }
}
