const axios = require("axios");
const SignVoteList = require("../helpers/signdata");
const State = require("../helpers/state");

exports.SendVoteList = () => {
  var data = State.votes;
  const sign = SignVoteList.sign_vote_list();
  const publicKey = process.env.POLLING_STATION_PUBLIC_KEY;
  State.votes = [];
  State.voteListHash = [];
  if (data && data !== null && data.length > 0) {
    axios
      .post("http://localhost:5000/node/receiveVoteList", {
        pollingStationPublicKey: publicKey,
        signature: sign,
        votes: data,
      })
      .then((res) => {
        console.log(`This vote list was successfully sended: ${res.status}`);
      })
      .catch((error) => {
        console.error(error);
      });
  }
};
