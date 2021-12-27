exports.receive_vote_list = (req, res, next) => {
  return res.status(200).json({
    Message: "receive_vote_list succesfully worked",
  });
};

exports.receive_block = (req, res, next) => {
  return res.status(200).json({
    Message: "receive_block succesfully worked",
  });
};

exports.get_block_chain = (req, res, next) => {
  return res.status(200).json({
    Message: "get_block_chain succesfully worked",
  });
};

exports.get_last_block = (req, res, next) => {
  return res.status(200).json({
    Message: "get_last_block succesfully worked",
  });
};

exports.get_genesis_block = (req, res, next) => {
  return res.status(200).json({
    Message: "get_genesis_block succesfully worked",
  });
};

exports.get_block = (req, res, next) => {
  return res.status(200).json({
    Message: "get_block succesfully worked",
  });
};

exports.vote_results = (req, res, next) => {
  return res.status(200).json({
    Message: "vote_results succesfully worked",
  });
};
