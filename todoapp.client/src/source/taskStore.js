import { configureStore } from '@reduxjs/toolkit';
import todoReducer from './todoSlice.js';

export const taskStore = configureStore({
    reducer: {
        todo: todoReducer,
    },
});
