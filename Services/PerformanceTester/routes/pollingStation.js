import express from 'express';
const router = express.Router();

import constroller from '../controllers/pollingStation.js';

router.route('/')
    .get(constroller.startTest)
    .post(constroller.fakePoint);

export default router;