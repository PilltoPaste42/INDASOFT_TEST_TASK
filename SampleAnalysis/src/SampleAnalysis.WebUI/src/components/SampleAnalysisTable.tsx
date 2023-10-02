import { Ref, forwardRef, useImperativeHandle, useState } from 'preact/compat';

export interface SampleAnalysisTableRefType {
  getData: (batchNumber?: string, beginDate?: string, endDate?: string) => void;
}

type DataResult = {
  sampleParameters: string[],
  sampleResults: ({
    batchNumber: string,
    sampleValues: number[]
  })[]
};

const SampleAnalysisTable = (props, ref: Ref<SampleAnalysisTableRefType>) => {
  const [isLoading, setLoading] = useState(true);
  const [data, setData] = useState<DataResult>(null);

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

                  {data.sampleParameters.map((param) => 
                  <th>{param}</th>

                  )}
                </tr>
              </thead>
              <tbody>
                {data.sampleResults.map(result =>
                  <tr key={result.batchNumber}>
                    <td>{result.batchNumber}</td>
                    {result.sampleValues.map(val =>
                      <td>{val}</td>)}
                  </tr>)}

              </tbody>
            </table>
          </div>
      }
    </div>
  ); 
};

export default forwardRef(SampleAnalysisTable);
