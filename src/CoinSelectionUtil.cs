using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
/// <summary>
/// cfd library namespace.
/// </summary>
namespace Cfd
{
  public static class CoinSelectionUtil
  {
    public static UtxoData[] SelectCoins(UtxoData[] utxoList, long txFeeAmount, long targetAmount,
      double effectiveFeeRate)
    {
      return SelectCoins(utxoList, txFeeAmount, targetAmount, effectiveFeeRate,
        effectiveFeeRate, -1, -1);
    }

    public static UtxoData[] SelectCoins(UtxoData[] utxoList, long txFeeAmount, long targetAmount,
      double effectiveFeeRate, double longTermFeeRate, long dustFeeRate, long knapsackMinChange)
    {
      if (utxoList is null)
      {
        throw new ArgumentNullException(nameof(utxoList));
      }
      if (utxoList.Length <= 0)
      {
        throw new InvalidOperationException("utxoList is empty.");
      }
      using (var handle = new ErrorHandle())
      {
        var ret = NativeMethods.CfdInitializeCoinSelection(
          handle.GetHandle(), (uint)utxoList.Length, 1, "", txFeeAmount,
          effectiveFeeRate, longTermFeeRate, dustFeeRate, knapsackMinChange,
          out IntPtr coinSelectHandle);
        if (ret != CfdErrorCode.Success)
        {
          handle.ThrowError(ret);
        }
        try
        {
          for (uint index = 0; index < utxoList.Length; ++index)
          {
            ret = NativeMethods.CfdAddCoinSelectionUtxo(
              handle.GetHandle(), coinSelectHandle, index,
              utxoList[index].GetOutPoint().GetTxid().ToHexString(),
              utxoList[index].GetOutPoint().GetVout(),
              utxoList[index].GetAmount(), "",
              utxoList[index].GetDescriptor().ToString());
            if (ret != CfdErrorCode.Success)
            {
              handle.ThrowError(ret);
            }
          }
          ret = NativeMethods.CfdAddCoinSelectionAmount(
            handle.GetHandle(), coinSelectHandle, 0, targetAmount, "");
          if (ret != CfdErrorCode.Success)
          {
            handle.ThrowError(ret);
          }

          ret = NativeMethods.CfdFinalizeCoinSelection(
            handle.GetHandle(), coinSelectHandle, out long utxoFeeAmount);
          if (ret != CfdErrorCode.Success)
          {
            handle.ThrowError(ret);
          }

          uint[] collectIndexes = new uint[utxoList.Length];
          uint collectCount = 0;
          for (uint index = 0; index < utxoList.Length; ++index)
          {
            ret = NativeMethods.CfdGetSelectedCoinIndex(
              handle.GetHandle(), coinSelectHandle,
              index, out int utxoIndex);
            if (ret != CfdErrorCode.Success)
            {
              handle.ThrowError(ret);
            }
            if (utxoIndex < 0)
            {
              break;
            }
            if (utxoList.Length >= utxoIndex)
            {
              throw new InvalidProgramException("utxoIndex maximum over.");
            }
            ++collectCount;
            collectIndexes[index] = (uint)utxoIndex;
          }

          /*
          ret = NativeMethods.CfdGetSelectedCoinAssetAmount(
            handle.GetHandle(), coinSelectHandle, 0, out long collectAmount);
          if (ret != CfdErrorCode.Success)
          {
            handle.ThrowError(ret);
          }
          */

          UtxoData[] selectedUtxoList = new UtxoData[collectCount];
          for (uint index = 0; index < collectCount; ++index)
          {
            selectedUtxoList[index] = utxoList[collectIndexes[index]];
          }
          return selectedUtxoList;
        }
        finally
        {
          NativeMethods.CfdFreeCoinSelectionHandle(handle.GetHandle(), coinSelectHandle);
        }
      }
    }

    public static ElementsUtxoData[] SelectCoinsForElements(
      ElementsUtxoData[] utxoList,
      IDictionary<ConfidentialAsset, long> targetAssetAmountMap,
      ConfidentialAsset feeAsset, long txFeeAmount,
      double effectiveFeeRate)
    {
      return SelectCoinsForElements(utxoList, targetAssetAmountMap, feeAsset, txFeeAmount,
        effectiveFeeRate, effectiveFeeRate, -1, -1);
    }

    public static ElementsUtxoData[] SelectCoinsForElements(
      ElementsUtxoData[] utxoList,
      IDictionary<ConfidentialAsset, long> targetAssetAmountMap,
      ConfidentialAsset feeAsset, long txFeeAmount,
      double effectiveFeeRate, double longTermFeeRate,
      long dustFeeRate, long knapsackMinChange)
    {
      if (utxoList is null)
      {
        throw new ArgumentNullException(nameof(utxoList));
      }
      if (utxoList.Length <= 0)
      {
        throw new InvalidOperationException("utxoList is empty.");
      }
      if (targetAssetAmountMap is null)
      {
        throw new ArgumentNullException(nameof(targetAssetAmountMap));
      }
      if (targetAssetAmountMap.Count <= 0)
      {
        throw new InvalidOperationException("targetAssetAmountMap is empty.");
      }
      if (feeAsset is null)
      {
        throw new ArgumentNullException(nameof(feeAsset));
      }
      if (feeAsset.HasBlinding())
      {
        throw new InvalidOperationException(
          "fee asset has blinding. fee asset is unblind only.");
      }
      using (var handle = new ErrorHandle())
      {
        var ret = NativeMethods.CfdInitializeCoinSelection(
          handle.GetHandle(), (uint)utxoList.Length, (uint)targetAssetAmountMap.Count,
          feeAsset.ToHexString(), txFeeAmount,
          effectiveFeeRate, longTermFeeRate, dustFeeRate, knapsackMinChange,
          out IntPtr coinSelectHandle);
        if (ret != CfdErrorCode.Success)
        {
          handle.ThrowError(ret);
        }
        try
        {
          for (uint index = 0; index < utxoList.Length; ++index)
          {
            ret = NativeMethods.CfdAddCoinSelectionUtxo(
              handle.GetHandle(), coinSelectHandle, index,
              utxoList[index].GetOutPoint().GetTxid().ToHexString(),
              utxoList[index].GetOutPoint().GetVout(),
              utxoList[index].GetAmount(), "",
              utxoList[index].GetDescriptor().ToString());
            if (ret != CfdErrorCode.Success)
            {
              handle.ThrowError(ret);
            }
          }
          uint assetIndex = 0;
          foreach (var key in targetAssetAmountMap.Keys)
          {
            ret = NativeMethods.CfdAddCoinSelectionAmount(
              handle.GetHandle(), coinSelectHandle, assetIndex,
              targetAssetAmountMap[key], key.ToHexString());
            if (ret != CfdErrorCode.Success)
            {
              handle.ThrowError(ret);
            }
            ++assetIndex;
          }

          ret = NativeMethods.CfdFinalizeCoinSelection(
            handle.GetHandle(), coinSelectHandle, out long utxoFeeAmount);
          if (ret != CfdErrorCode.Success)
          {
            handle.ThrowError(ret);
          }

          uint[] collectIndexes = new uint[utxoList.Length];
          uint collectCount = 0;
          for (uint index = 0; index < utxoList.Length; ++index)
          {
            ret = NativeMethods.CfdGetSelectedCoinIndex(
              handle.GetHandle(), coinSelectHandle,
              index, out int utxoIndex);
            if (ret != CfdErrorCode.Success)
            {
              handle.ThrowError(ret);
            }
            if (utxoIndex < 0)
            {
              break;
            }
            if (utxoList.Length >= utxoIndex)
            {
              throw new InvalidProgramException("utxoIndex maximum over.");
            }
            ++collectCount;
            collectIndexes[index] = (uint)utxoIndex;
          }
          /*
          assetIndex = 0;
          collectAmountList = new Dictionary<ConfidentialAsset, long>();
          foreach (var key in targetAssetAmountMap.Keys)
          {
            ret = NativeMethods.CfdGetSelectedCoinAssetAmount(
              handle.GetHandle(), coinSelectHandle, assetIndex,
              out long collectAmount);
            if (ret != CfdErrorCode.Success)
            {
              handle.ThrowError(ret);
            }
            ++assetIndex;
            collectAmountList.Add(key, collectAmount);
          }
          */

          ElementsUtxoData[] selectedUtxoList = new ElementsUtxoData[collectCount];
          for (uint index = 0; index < collectCount; ++index)
          {
            selectedUtxoList[index] = utxoList[collectIndexes[index]];
          }
          return selectedUtxoList;
        }
        finally
        {
          NativeMethods.CfdFreeCoinSelectionHandle(handle.GetHandle(), coinSelectHandle);
        }
      }
    }
  }
}
