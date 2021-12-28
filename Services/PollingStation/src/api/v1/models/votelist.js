const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");

class VoteList {
  constructor(pollingStationPublicKey, signature, votes) {
    this.pollingStationPublicKey = pollingStationPublicKey;
    this.signature = signature;
    this.votes = votes;
  }

  calculateHash() {
    let hashItem = "";
    for (var item of this.votes) {
      hashItem += SHA256(
        item.publicKey + item.candinateId + item.signature
      ).toString();
    }
    return SHA256(hashItem).toString();
  }

  isValid() {
    if (
      this.pollingStationPublicKey === null ||
      this.votes === null ||
      this.votes === []
    )
      return false;

    if (!this.signature || this.signature.length === 0) {
      throw new Error("No signature in this vote list");
    }

    var calculateHash = this.calculateHash();
    const publicKey = ec.keyFromPublic(this.pollingStationPublicKey, "hex");
    return publicKey.verify(calculateHash, this.signature);
  }
}

module.exports.VoteList = VoteList;
