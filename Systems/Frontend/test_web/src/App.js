import './App.css';
import UnitList from './components/UnitList';
import SyncForm from './components/SyncForm';

function App() {
  return (
    <div className="App">
      <header>
        <div className="sync-form">
          <SyncForm />
        </div>
        <div className="unit-list-container">
          <UnitList />
        </div>
      </header>
    </div>
  );
}

export default App;
