import express from 'express';
import morgan from 'morgan';
const app = express();
import pollingStation from './routes/pollingStation.js';

app.use(express.static('./public'));
app.use(morgan('tiny'));
app.use(express.json());

app.use('/pollingStation', pollingStation);

app.listen(3008);