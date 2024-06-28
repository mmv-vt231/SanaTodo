import { useState, useEffect } from "react";

export const useSubscribe = observable => {
  const [state, setState] = useState({});

  useEffect(() => {
    const sub = observable.subscribe(setState);
    return () => sub.unsubscribe();
  }, [observable]);

  return state;
};
