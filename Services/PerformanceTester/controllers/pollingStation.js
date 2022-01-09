import { generateVote } from '../services/helper.js';
import { getCandidates } from '../services/candidate.js'
import fs from 'fs/promises'
import axios from 'axios';

axios.interceptors.request.use(x => {
    x.meta = x.meta || {}
    x.meta.requestStartedAt = new Date().getTime();
    return x;
})


axios.interceptors.response.use(x => {
    x.responseTime = new Date().getTime() - x.config.meta.requestStartedAt;
    return x;
})

const startTest = async (req, res) => {
    const candidates = await getCandidates();
    const requests = [];
    for (let i = 0; i < 1; i++) {
        let candidate = candidates[Math.floor(Math.random() * candidates.length)];
        let vote = generateVote(candidate.id);
        console.log(vote);
        // requests.push(axios.post("http://localhost:3008/pollingStation", vote));
        requests.push(axios.post("http://192.168.43.254:3000/pollingstation/recevieVote", vote));
    }
    Promise.all(requests).then(async (values) => {
        values = values.map(x => x.responseTime);
        await fs.writeFile("./public/results.json", JSON.stringify(values));
        res.status(200).json({ success: true, values });
    });
}

const fakePoint = (req, res) => {
    console.log(req.body);
    res.status(200).json({ success: true });
}

export default {
    startTest,
    fakePoint
}
