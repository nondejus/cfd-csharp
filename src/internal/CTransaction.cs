using System;
using System.Runtime.InteropServices;

namespace Cfd
{
  /// <summary>
  /// cfd library (transaction) access class.
  /// </summary>
  internal static class CTransaction
  {
    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdInitializeTransaction(
        [In] IntPtr handle,
        [In] int networkType,
        [In] uint version,
        [In] uint locktime,
        [In] string txHexString,
        [Out] out IntPtr createHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddTransactionInput(
        [In] IntPtr handle,
        [In] IntPtr createHandle,
        [In] string txid,
        [In] uint vout,
        [In] uint sequence);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddTransactionOutput(
        [In] IntPtr handle,
        [In] IntPtr createHandle,
        [In] long satoshiValue,
        [In] string address,
        [In] string directLockingScript,
        [In] string asset);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFinalizeTransaction(
        [In] IntPtr handle,
        [In] IntPtr createHandle,
        [Out] out IntPtr txHexString);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFreeTransactionHandle(
        [In] IntPtr handle,
        [In] IntPtr createHandle);

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

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddSignWithPrivkeySimple(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] string txid,
        [In] uint vout,
        [In] int hashType,
        [In] string pubkey,
        [In] string privkey,
        [In] long satoshiValue,
        [In] int sighashType,
        [In] bool sighashAnyoneCanPay,
        [In] bool hasGrindR,
        [Out] out IntPtr txString);

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

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdCreateSighash(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] string txid,
        [In] uint vout,
        [In] int hashType,
        [In] string pubkey,
        [In] string redeemScript,
        [In] long satoshiValue,
        [In] int sighashType,
        [In] bool sighashAnyoneCanPay,
        [Out] out IntPtr sighash);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxInfo(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [Out] out IntPtr txid,
        [Out] out IntPtr wtxid,
        [Out] out uint size,
        [Out] out uint vsize,
        [Out] out uint weight,
        [Out] out uint version,
        [Out] out uint locktime);
      
    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxIn(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] uint index,
        [Out] out IntPtr txid,
        [Out] out uint vout,
        [Out] out uint sequence,
        [Out] out IntPtr scriptSig);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxInWitness(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] uint txinIndex,
        [In] uint stackIndex,
        [Out] out IntPtr stackData);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxOut(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] uint index,
        [Out] out long satoshiValue,
        [Out] out IntPtr lockingScript);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxInCount(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [Out] out uint count);
      
    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxInWitnessCount(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] uint index,
        [Out] out uint count);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxOutCount(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [Out] out uint count);
      
    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxInIndex(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] string txid,
        [In] uint vout,
        [Out] out uint index);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetTxOutIndex(
        [In] IntPtr handle,
        [In] int networkType,
        [In] string txHexString,
        [In] string address,
        [In] string directLockingScript,
        [Out] out uint index);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdInitializeFundRawTx(
        [In] IntPtr handle,
        [In] int networkType,
        [In] uint targetAssetCount,
        [In] string feeAsset,
        [Out] out IntPtr fundHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddTxInForFundRawTx(
        [In] IntPtr handle,
        [In] IntPtr fundHandle,
        [In] string txid,
        [In] uint vout,
        [In] long amount,
        [In] string descriptor,
        [In] string asset,
        [In] bool isIssuance,
        [In] bool isBlindIssuance,
        [In] bool isPegin,
        [In] uint peginBtcTxSize,
        [In] string fedpegScript);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddUtxoForFundRawTx(
        [In] IntPtr handle,
        [In] IntPtr fundHandle,
        [In] string txid,
        [In] uint vout,
        [In] long amount,
        [In] string descriptor,
        [In] string asset);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddTargetAmountForFundRawTx(
        [In] IntPtr handle,
        [In] IntPtr fundHandle,
        [In] uint assetIndex,
        [In] long amount,
        [In] string asset,
        [In] string reservedAddress);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdSetOptionFundRawTx(
        [In] IntPtr handle,
        [In] IntPtr fundHandle,
        [In] int key,
        [In] long int64Value,
        [In] double doubleValue,
        [In] bool boolValue);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFinalizeFundRawTx(
        [In] IntPtr handle,
        [In] IntPtr fundHandle,
        [In] string txHex,
        [In] double effectiveFeeRate,
        [Out] out long txFee,
        [Out] out uint appendTxOutCount,
        [Out] out IntPtr outputTxHex);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetAppendTxOutFundRawTx(
        [In] IntPtr handle,
        [In] IntPtr fundHandle,
        [In] uint index,
        [Out] out IntPtr appendAddress);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFreeFundRawTxHandle(
        [In] IntPtr handle,
        [In] IntPtr fundHandle);
  }
}
