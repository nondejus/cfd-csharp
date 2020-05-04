using System;

namespace Cfd
{
  /// <summary>
  /// txid data class.
  /// </summary>
  public class ElementsUtxoData : UtxoData, IEquatable<ElementsUtxoData>
  {
    private readonly ConfidentialValue value;
    private readonly ConfidentialAsset asset;
    private readonly string unblindedAsset;
    private readonly bool isIssuance;
    private readonly bool isBlindIssuance;
    private readonly bool isPegin;
    private readonly uint peginBtcTxSize;
    private readonly Script fedpegScript;

    /// <summary>
    /// Constructor. (empty)
    /// </summary>
    public ElementsUtxoData()
    {
      // do nothing
    }

    public ElementsUtxoData(OutPoint outpoint) : base(outpoint)
    {
    }

    public ElementsUtxoData(OutPoint outpoint, Descriptor descriptor) : base(outpoint, descriptor)
    {
    }

    public ElementsUtxoData(OutPoint outpoint, ConfidentialAsset asset, long amount) : base(outpoint, amount)
    {
      if (asset is null)
      {
        throw new ArgumentNullException(nameof(asset));
      }
      if (asset.HasBlinding())
      {
        throw new InvalidOperationException("asset is blinded.");
      }
      this.asset = asset;
      unblindedAsset = asset.ToHexString();
      value = new ConfidentialValue(amount);
    }

    public ElementsUtxoData(OutPoint outpoint, ConfidentialAsset asset, ConfidentialValue value)
         : base(outpoint, ((value is null) ? 0 : value.GetSatoshiValue()))
    {
      if (asset is null)
      {
        throw new ArgumentNullException(nameof(asset));
      }
      if (value is null)
      {
        throw new ArgumentNullException(nameof(value));
      }
      this.asset = asset;
      this.value = value;
      if (!asset.HasBlinding())
      {
        unblindedAsset = asset.ToHexString();
      }
    }

    public ElementsUtxoData(OutPoint outpoint, ConfidentialAsset asset, ConfidentialValue value, Descriptor descriptor)
         : base(outpoint, ((value is null) ? 0 : value.GetSatoshiValue()), descriptor)
    {
      if (asset is null)
      {
        throw new ArgumentNullException(nameof(asset));
      }
      if (value is null)
      {
        throw new ArgumentNullException(nameof(value));
      }
      this.asset = asset;
      this.value = value;
    }

    public ElementsUtxoData(OutPoint outpoint, string asset,
        long amount, ConfidentialAsset assetCommitment, ConfidentialValue valueCommitment)
          : base(outpoint, amount)
    {
      if (asset is null)
      {
        throw new ArgumentNullException(nameof(asset));
      }
      if (assetCommitment is null)
      {
        throw new ArgumentNullException(nameof(assetCommitment));
      }
      if (valueCommitment is null)
      {
        throw new ArgumentNullException(nameof(valueCommitment));
      }
      unblindedAsset = asset;
      this.asset = assetCommitment;
      value = valueCommitment;
    }

    public ElementsUtxoData(OutPoint outpoint, string asset,
        long amount, ConfidentialAsset assetCommitment, ConfidentialValue valueCommitment,
        Descriptor descriptor)
          : base(outpoint, amount, descriptor)
    {
      if (asset is null)
      {
        throw new ArgumentNullException(nameof(asset));
      }
      if (assetCommitment is null)
      {
        throw new ArgumentNullException(nameof(assetCommitment));
      }
      if (valueCommitment is null)
      {
        throw new ArgumentNullException(nameof(valueCommitment));
      }
      unblindedAsset = asset;
      this.asset = assetCommitment;
      value = valueCommitment;
    }

    public ElementsUtxoData(OutPoint outpoint, string asset,
        long amount, ConfidentialAsset assetCommitment, ConfidentialValue valueCommitment,
        Descriptor descriptor, bool isBlindIssuance)
          : this(outpoint, asset, amount, assetCommitment, valueCommitment, descriptor)
    {
      this.isIssuance = true;
      this.isBlindIssuance = isBlindIssuance;
    }

    public ElementsUtxoData(OutPoint outpoint, string asset,
        long amount, ConfidentialAsset assetCommitment, ConfidentialValue valueCommitment,
        Descriptor descriptor, uint peginBtcTxSize, Script fedpegScript)
          : this(outpoint, asset, amount, assetCommitment, valueCommitment, descriptor)
    {
      if (fedpegScript is null)
      {
        throw new ArgumentNullException(nameof(fedpegScript));
      }
      this.isPegin = true;
      this.peginBtcTxSize = peginBtcTxSize;
      this.fedpegScript = fedpegScript;
    }

    public string GetAsset()
    {
      return unblindedAsset;
    }

    public ConfidentialAsset GetAssetCommitment()
    {
      return asset;
    }

    public ConfidentialValue GetValueCommitment()
    {
      return value;
    }

    public bool IsIssuance()
    {
      return isIssuance;
    }

    public bool IsBlindIssuance()
    {
      return isBlindIssuance;
    }

    public bool IsPegin()
    {
      return isPegin;
    }

    public uint GetPeginBtcTxSize()
    {
      return peginBtcTxSize;
    }

    public Script GetFedpegScript()
    {
      return fedpegScript;
    }

    public bool Equals(ElementsUtxoData other)
    {
      if (other is null)
      {
        return false;
      }
      if (Object.ReferenceEquals(this, other))
      {
        return true;
      }

      return (GetOutPoint() == other.GetOutPoint());
    }

    public override bool Equals(object obj)
    {
      if (obj is null)
      {
        return false;
      }
      if ((obj as ElementsUtxoData) != null)
      {
        return this.Equals((ElementsUtxoData)obj);
      }
      return false;
    }

    public override int GetHashCode()
    {
      return GetOutPoint().GetHashCode();
    }

    public static bool operator ==(ElementsUtxoData lhs, ElementsUtxoData rhs)
    {
      if (lhs is null)
      {
        if (rhs is null)
        {
          return true;
        }
        return false;
      }
      return lhs.Equals(rhs);
    }

    public static bool operator !=(ElementsUtxoData lhs, ElementsUtxoData rhs)
    {
      return !(lhs == rhs);
    }
  }
}
