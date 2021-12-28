const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");

module.exports.validate_signature = (
  pollingStationPublicKey,
  signature,
  voteList
) => {
  if (pollingStationPublicKey === null || voteList === null || voteList === [])
    return false;

  if (!signature || signature.length === 0) {
    throw new Error("No signature in this vote list");
  }
  let hashItem = "";
  for (var item of voteList) {
    hashItem += SHA256(
      item.publicKey + item.candinateId + item.signature
    ).toString();
  }
  calculateHash = SHA256(hashItem).toString();
  const publicKey = ec.keyFromPublic(pollingStationPublicKey, "hex");
  return publicKey.verify(calculateHash, signature);
};
