const getNextId = (() => {
    let id = 0;
    return () => ++id;
  })();
  
  export const generateUniqueId = () => {
    return getNextId();
  };