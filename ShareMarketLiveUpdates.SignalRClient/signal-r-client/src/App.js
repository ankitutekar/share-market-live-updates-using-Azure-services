import React, { useEffect, useState } from 'react';

import './App.css';
import { setupSignalR } from './signal-r-helper';
import SharesTable from './SharesTable';

const App = () => {
  let connection = null;
  const [shares, setShares] = useState([]);
  const [updatedShareId, setUpdatedShareId] = useState(null);

  const onSharesUpdated = (updatedShares) => {
    setShares(shares => {
      const newShares = shares.map((val, index) => {
        var updated =  updatedShares.find((updatedShare) => {
          return updatedShare.id === val.id;
        });
        if(updated)
          return updated;
        return val;
      });
      return newShares;
    });

    setUpdatedShareId(updatedShareId => {
      return updatedShares[0].id
    });
  };

  useEffect( () => {
    if(!connection) {
    fetch('http://localhost:5055/api/GetInitialPrices')
      .then(response => response.json())
      .then(data => {
          setShares(data);
          setupSignalR(connection, onSharesUpdated);
        }
      )
      .catch((err) => {
        console.log(err);
      });
    }
  }, [connection]);

  return (
    <div className="App">
      <SharesTable shares={[...shares]} updatedShareId={updatedShareId}/>
    </div>
  );
}

export default App;
