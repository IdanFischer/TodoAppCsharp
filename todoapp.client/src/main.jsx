import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { Provider } from 'react-redux';
import { taskStore } from './source/taskStore.js';

createRoot(document.getElementById('root')).render(
    <StrictMode>
        <Provider store={taskStore}>
            <App />
        </Provider>
    </StrictMode>
);
