import { Cryptography } from "./cryptography.js";
import Vote from './vote.js'
const generateKeyPair = () => {
    var cryptography = new Cryptography();
    const publicKey = cryptography.publicKey;
    const privateKey = cryptography.privateKey;
    return {
        publicKey,
        privateKey
    }
}

export const generateVote = (candidateId) => {
    const { publicKey, privateKey } = generateKeyPair();
    return new Vote(publicKey, privateKey, candidateId);
}
