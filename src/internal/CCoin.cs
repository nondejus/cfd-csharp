using System;
using System.Runtime.InteropServices;

namespace Cfd
{
  internal static class CCoin
  {
    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdInitializeCoinSelection(
        [In] IntPtr handle,
        [In] uint utxoCount,
        [In] uint targetAssetCount,
        [In] string feeAsset,
        [In] long tx_feeAmount,
        [In] double effectiveFeeRate,
        [In] double longTermFeeRate,
        [In] double dustFeeRate,
        [In] long knapsackMinChange,
        [Out] out IntPtr coinSelectHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddCoinSelectionUtxo(
        [In] IntPtr handle,
        [In] IntPtr coinSelectHandle,
        [In] uint utxoIndex,
        [In] string txid,
        [In] uint vout,
        [In] long amount,
        [In] string asset,
        [In] string descriptor);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddCoinSelectionAmount(
        [In] IntPtr handle,
        [In] IntPtr coinSelectHandle,
        [In] uint assetIndex,
        [In] long amount,
        [In] string asset);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFinalizeCoinSelection(
        [In] IntPtr handle,
        [In] IntPtr coinSelectHandle,
        [Out] out long utxoFeeAmount);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetSelectedCoinIndex(
        [In] IntPtr handle,
        [In] IntPtr coinSelectHandle,
        [In] uint index,
        [Out] out uint utxoIndex);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdGetSelectedCoinAssetAmount(
        [In] IntPtr handle,
        [In] IntPtr coinSelectHandle,
        [In] uint assetIndex,
        [Out] out long amount);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFreeCoinSelectionHandle(
        [In] IntPtr handle,
        [In] IntPtr coinSelectHandle);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdInitializeEstimateFee(
        [In] IntPtr handle,
        [Out] out IntPtr feeHandle,
        [In] bool isElements);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdAddTxInForEstimateFee(
        [In] IntPtr handle,
        [In] IntPtr feeHandle,
        [In] string txid,
        [In] uint vout,
        [In] string descriptor,
        [In] string asset,
        [In] bool isIssuance,
        [In] bool isBlindIssuance,
        [In] bool isPegin,
        [In] uint peginBtcTxSize,
        [In] string fedpegScript);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFinalizeEstimateFee(
        [In] IntPtr handle,
        [In] IntPtr feeHandle,
        [In] string txHex,
        [In] string feeAsset,
        [Out] out long txFee,
        [Out] out long utxoFee,
        [In] bool isBlind,
        [In] double effectiveFeeRate);

    [DllImport("cfd", CallingConvention = CallingConvention.StdCall)]
    internal static extern CfdErrorCode CfdFreeEstimateFeeHandle(
        [In] IntPtr handle,
        [In] IntPtr feeHandle);
  }
}
