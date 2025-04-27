import { useEffect, useState } from "react";
import "./App.css";
import axios from "axios";

function App() {
  const [data, setData] = useState<string>("");

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios.get<string>("/api/clients/test");
      console.log(result.data);
      setData(result.data);
    };

    fetchData();
  }, []);

  return (
    <div>
      <h1 id="tableLabel">Weather forecast</h1>
      <p>This component demonstrates fetching data from the server.</p>
      {data}
    </div>
  );
}

export default App;
