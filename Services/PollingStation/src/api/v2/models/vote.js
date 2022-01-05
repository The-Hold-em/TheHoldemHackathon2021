const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");
const mongoose = require("mongoose");
const Schema = mongoose.Schema;

const voteSchema = new Schema({
  votePublicKey: {
    type: String,
    required: true,
    length: [130, "votePublicKey length's is must be 130 characters"],
  },
  candinateId: { type: String, required: true },
  voteSignature: {
    type: String,
    required: true,
    length: [140, "voteSignature length's is must be 140 characters"],
  },
  voteHash: {
    type: String,
    required: false,
    length: [256, "voteHash length's is must be 256 characters"],
  },
});

const calculateHash = (data) => {
  return SHA256(data).toString();
};

/** isVaild fonksiyonu clientden gelen oyun doğruluğunu kontrol eder */
const isVaild = (publicKey, candinateId, signature) => {
  if (publicKey === null || candinateId === null) {
    return false;
  }

  if (!signature || signature.length === 0) {
    throw new Error("No signature in this vote");
  }
  var calculateHash = this.calculateHash(candinateId);
  const pk = ec.keyFromPublic(publicKey, "hex");

  return pk.verify(calculateHash, signature);
};

module.exports = {
  Vote: mongoose.model("Vote", voteSchema),
  CalculateHash: calculateHash,
  IsVaild: isVaild,
};
