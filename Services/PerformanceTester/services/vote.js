import SHA256 from "crypto-js/sha256.js";
import  EC from "elliptic";
const ec = new EC.ec("secp256k1");

export default class Vote {
    publicKey;
    candinateId;
    signature;

    constructor(publiKey, privateKey, candinateId) {
        this.publicKey = publiKey;
        this.candinateId = candinateId;
        this.signature = this.sign(privateKey);
    }

    calculateHash() {
        return SHA256(this.candinateId).toString();
    }

    sign(privateKey) {
        const signingKey = ec.keyFromPrivate(privateKey);
        if (signingKey.getPublic("hex") !== this.publicKey) {
            throw new Error("You cannot sign this vote!");
        }
        const hashVote = this.calculateHash();
        const sig = signingKey.sign(hashVote, "base64");
        return sig.toDER("hex");
    }

    isValid() {
        if (this.publicKey === null || this.candinateId === null) return false;

        if (!this.signature || this.signature.length === 0) {
            throw new Error("No signature in this vote");
        }
        var calculateHash = this.calculateHash();
        const publicKey = ec.keyFromPublic(this.publicKey, "hex");
        return publicKey.verify(calculateHash, this.signature);
    }
}
