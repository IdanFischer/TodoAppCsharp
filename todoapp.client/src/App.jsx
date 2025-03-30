import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const todoURL = "https://localhost:7282/todos"; // Update as needed
    const [todos, setTodos] = useState([]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [currentTask, setCurrentTask] = useState(null);
    //co

    useEffect(() => {
        populateTodoTasks();
    }, []);

    async function populateTodoTasks() {
        const response = await fetch(todoURL, {
            method: "GET",
            headers: {
                'Content-Type': 'application/json'
            },
        });
        if (response.ok) {
            const data = await response.json();
            setTodos(data);
        }
    }

    const openModalForAdd = () => {
        setCurrentTask({ title: '', description: '', isComplete: false });
        setIsModalOpen(true);
    };

    const openModalForEdit = (task) => {
        setCurrentTask(task);
        setIsModalOpen(true);
    };

    const closeModal = () => {
        setCurrentTask(null);
        setIsModalOpen(false);
    };

    const handleSubmit = async (task) => {
        if (task.id) {
            // Edit existing task
            await fetch(todoURL, {
                method: "PUT",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(task)
            });
        } else {
            // Create new task
            await fetch(todoURL, {
                method: "POST",
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(task)
            });
        }
        closeModal();
        populateTodoTasks();
    };

    const handleDelete = async (task) => {
        await fetch(todoURL, {
            method: "DELETE",
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(task)
        });
        populateTodoTasks();
    };

    return (
        <div className="container">
            <h1>Todo</h1>
            <p>The tasks you need to do</p>
            <button className="btn" onClick={openModalForAdd}>Add Task</button>
            {todos.length === 0 ? (
                <p>Loading tasks...</p>
            ) : (
                <table className="table">
                    <thead>
                        <tr>
                            <th>Title</th>
                            <th>Description</th>
                            <th>Complete</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {todos.map(task => (
                            <tr key={task.id}>
                                <td>{task.title}</td>
                                <td>{task.description}</td>
                                <td>{task.isComplete ? 'Yes' : 'No'}</td>
                                <td>
                                    <button className="btn-edit" onClick={() => openModalForEdit(task)}>Edit</button>
                                    <button className="btn btn-danger" onClick={() => handleDelete(task)}>Delete</button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )}
            {isModalOpen && (
                <TodoModal
                    task={currentTask}
                    onClose={closeModal}
                    onSubmit={handleSubmit}
                />
            )}
        </div>
    );
}

function TodoModal({ task, onClose, onSubmit }) {
    // Make a local copy of the task for form editing
    const [formData, setFormData] = useState(task);

    // Update formData when the passed task changes (for edit mode)
    useEffect(() => {
        setFormData(task);
    }, [task]);

    const handleChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value
        }));
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(formData);
    };

    return (
        <div className="modal-overlay">
            <div className="modal">
                <h2>{formData.id ? 'Edit Task' : 'Add Task'}</h2>
                <form onSubmit={handleSubmit}>
                    <div className="form-group">
                        <label>{formData.id ? 'Edit Task Title' : 'Title'}</label>
                        <input
                            name="title"
                            type="text"
                            value={formData.title}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label>Description</label>
                        <textarea
                            name="description"
                            value={formData.description}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div
                        className="form-group"
                        id="checkbox-form-group">
                        <label id="complete-label">
                            <p>Completed</p>
                            <input
                                name="isComplete"
                                id="complete-check"
                                type="checkbox"
                                checked={formData.isComplete}
                                onChange={handleChange}
                            /> 
                        </label>
                    </div>
                    <div className="modal-actions">
                        <button type="submit" className={formData.id ? 'btn-edit' : 'btn'}>
                            {formData.id ? 'Update' : 'Add'}
                        </button>
                        <button type="button" className="btn btn-danger" onClick={onClose}>Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default App;
