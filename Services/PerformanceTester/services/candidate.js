import axios from 'axios';
export const getCandidates=async ()=>{
    return await axios.get(`http://localhost:8264/api/candidate`).then(x=>x.data.data);
}