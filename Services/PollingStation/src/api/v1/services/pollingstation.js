const axios = require("axios");
const SignVoteList = require("../helpers/signdata");
const VoteList = require("../helpers/state");
exports.SendVoteList = () => {
  const data = VoteList.voteList;
  const sign = SignVoteList.sign_data(data);
  const publicKey = process.env.POLLING_STATION_PUBLIC_KEY;
  VoteList.voteList = [];

  axios
    .post("http://localhost:3000/pollingstation/sendVoteListToNode", {
      pollingStationPublicKey: publicKey,
      signature: sign,
      voteList: data,
    })
    .then((res) => {
      console.log(`statusCode: ${res.status}`);
      console.log(res);
    })
    .catch((error) => {
      console.error(error);
    });
};
