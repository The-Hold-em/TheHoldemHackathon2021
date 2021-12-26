const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");

module.exports.validate_signature = (
  voterPublicKey,
  candinateId,
  signature
) => {
  if (voterPublicKey === null || candinateId === null) return false;

  if (!signature || signature.length === 0) {
    throw new Error("No signature in this vote");
  }
  calculateHash = SHA256(candinateId).toString();
  const publicKey = ec.keyFromPublic(voterPublicKey, "hex");
  return publicKey.verify(calculateHash, signature);
};
