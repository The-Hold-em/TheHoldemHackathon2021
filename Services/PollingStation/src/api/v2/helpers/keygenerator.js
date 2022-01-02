const EC = require("elliptic");
const ec = EC.ec("secp256k1");

const key = ec.genKeyPair();

const publicKey = key.getPublic("hex");
const privateKey = key.getPrivate("hex");

console.log();
console.log("Private Key:", privateKey);

console.log();
console.log("Public Key:", publicKey);
