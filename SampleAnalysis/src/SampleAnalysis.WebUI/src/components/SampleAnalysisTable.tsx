import { Ref, forwardRef, useEffect, useImperativeHandle, useState } from 'preact/compat';

export interface SampleAnalysisTableRefType {
  getData: (batchNumber?: string, beginDate?: string, endDate?: string) => void;
}

const SampleAnalysisTable = (props, ref: Ref<SampleAnalysisTableRefType>) => {
  const [text, setText] = useState('null');
  const [isLoading, setLoading] = useState(true);
  const [data, setData] = useState([]);

  useImperativeHandle(ref, () => {
    return {
      getData(batchNumber?: string, beginDate?: string, endDate?: string) {
        setLoading(true);
        fetch('/api/SampleAnalysis?' + new URLSearchParams({
          batchNumber : batchNumber,
          beginDate : beginDate,
          endDate : endDate
        }))
          .then((response) => response.json())
          .then((actualData) => setData(actualData))
          .finally(() => {setLoading(false)});
      },
    };
  });

  

  return(
    <div>
      {
        isLoading ? 
        <div> Загрузка таблицы... </div> 
        : <div>
            <table>
              <thead>
                <tr>
                  <th>Номер партии</th>
                  {data[0].parameters.map(item => 
                    <th>{item}</th>
                  )}
                </tr>
              </thead>
              <tbody>
                {data.map(batch => 
                <tr key={batch.batchNumber}>
                  <td>{batch.batchNumber}</td>
                  {batch.values.map(val =>
                    <td>{val}</td>  
                  )}
                </tr>
                )}
              </tbody>
            </table>
          </div>
      }
    </div>
  ); 
};

export default forwardRef(SampleAnalysisTable);
