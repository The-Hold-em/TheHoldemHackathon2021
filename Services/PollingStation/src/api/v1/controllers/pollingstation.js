const Vote = require("../models/vote");
const ValidateSignature = require("../helpers/validatesignature");

exports.recevie_vote = (req, res, next) => {
  const publicKey = req.body.publicKey;
  const candinateId = req.body.candinateId;
  const signature = req.body.signature;
  if (ValidateSignature.validate_signature(publicKey, candinateId, signature)) {
    return res.status(200).json({
      Message: "This vote is successfully validated",
    });
  } else {
    return res.status(409).json({
      Message: "This vote validation is failed",
    });
  }
};
