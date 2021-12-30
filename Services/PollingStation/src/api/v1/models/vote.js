const SHA256 = require("crypto-js/sha256.js");
const EC = require("elliptic");
const ec = EC.ec("secp256k1");

class Vote {
  constructor(publicKey, candinateId, signature) {
    this.publicKey = publicKey;
    this.candinateId = candinateId;
    this.signature = signature;
  }
  calculateHash() {
    return SHA256(this.candinateId).toString();
  }

  isValid() {
    if (this.publicKey === null || this.candinateId === null) {
      return false;
    }

    if (!this.signature || this.signature.length === 0) {
      console.log(
        "*********************************************************************"
      );
      throw new Error("No signature in this vote");
    }
    var calculateHash = this.calculateHash();
    const publicKey = ec.keyFromPublic(this.publicKey, "hex");

    return publicKey.verify(calculateHash, this.signature);
  }
}

module.exports.Vote = Vote;
