import { render } from 'preact';
import { useRef, useState } from 'preact/hooks';
import './style.css';
import './custom.css';

import SampleAnalysisTable, { SampleAnalysisTableRefType } from './components/SampleAnalysisTable';

const App = () => {
  const [formData, updateFormData] = useState(
    Object.freeze({
      batchNumber: '',
      beginDate: '',
      endDate: '',
    })
  );
  const [visible, setVisible] = useState(false);

  const tableRef = useRef<SampleAnalysisTableRefType>(null);
  const getData = (obj = { batchNumber: '', beginDate: '', endDate: '' }) => {
    if (tableRef.current) {
      tableRef.current.getData(obj.batchNumber, obj.beginDate, obj.endDate);
    }
  };

  const onSubmit = (ev) => {
    ev.preventDefault();
    setVisible(true);
    getData(formData);
  };

  const onChange = (ev) => {
    updateFormData({
      ...formData,
      [ev.target.name]: ev.target.value.trim(),
    });
  };

  return (
    <div class="card">
      <form onSubmit={onSubmit}>
        <div class="row">
          <label>Номер партии: </label>
          <input type="text" name="batchNumber" onChange={onChange} />
        </div>
        <div class="row">
          <label>Дата производства (с): </label>
          <input type="date" name="beginDate" onChange={onChange} />
        </div>
        <div class="row">
          <label>Дата производства (по): </label>
          <input type="date" name="endDate" onChange={onChange} />
        </div>
        <div class="row">
          <button type="submit">Создать отчет</button>
        </div>
      </form>
      <div className={!visible ? 'hidden' : ''}>
        <SampleAnalysisTable ref={tableRef} />
      </div>
    </div>
  );
};

render(<App />, document.getElementById('app'));
