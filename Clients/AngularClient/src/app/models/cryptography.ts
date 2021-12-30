import * as EC from "elliptic";
const ec = new EC.ec("secp256k1");

export class Cryptography {
  key: any;
  publicKey: string = "";
  privateKey: string = "";
  constructor() {
    this.key = ec.genKeyPair();
    this.publicKey = this.key.getPublic("hex");
    this.privateKey = this.key.getPrivate("hex");
  }
  getPublicKey() {
    return this.key.getPublic("hex");
  }
  getPrivateKey() {
    return this.key.getPrivate("hex");
  }
}
