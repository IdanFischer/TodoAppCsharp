import { createSlice } from '@reduxjs/toolkit';

const initialState = {
    todos: [],
    isModalOpen: false,
    currentTask: null,
};

const todoSlice = createSlice({
    name: 'todo',
    initialState,
    reducers: {
        setTodos(state, action) {
            state.todos = action.payload;
        },
        openModalForAdd(state) {
            state.currentTask = { title: '', description: '', isComplete: false };
            state.isModalOpen = true;
        },
        openModalForEdit(state, action) {
            state.currentTask = action.payload;
            state.isModalOpen = true;
        },
        closeModal(state) {
            state.currentTask = null;
            state.isModalOpen = false;
        },
    },
});

export const { setTodos, openModalForAdd, openModalForEdit, closeModal } = todoSlice.actions;
export default todoSlice.reducer;
