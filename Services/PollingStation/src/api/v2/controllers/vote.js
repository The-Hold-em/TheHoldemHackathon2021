const { Vote, CalculateHash, IsVaild } = require("../models/Vote");

exports.recevie_vote = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;

  if (IsVaild()) {
    Vote.create({
      votePublicKey: publicKey,
      candinateId: candinateId,
      voteSignature: signature,
      voteHash: CalculateHash(publicKey + candinateId + signature),
    });
    return res.status(200).json({
      Message: "This vote validation is success",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};
