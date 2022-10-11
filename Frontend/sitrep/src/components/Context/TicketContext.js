import axios from "axios";
import React, { createContext, useEffect, useState } from "react";
import { API_ENDPOINT } from "../../App";

const TicketContext = createContext({});
const status = ["OPEN", "IN_PROGRESS", "RESOLVED", "CLOSED"];
const type = ["TASK", "BUG", "REQUEST", "OTHER"];
const priority = ["LOW", "MEDIUM", "HIGH"];
const category = ["new feature", "bugfix"];
//gonna have to replace this with a fetch all users

export const TicketProvider = ({ children }) => {
  //   const [users, setUsers] = useState("");
  //   useEffect(() => {
  //     axios.get(`${API_ENDPOINT}/api/Auth/users`).then((result) => {
  //       console.log(result.data + "RESULT!!!");
  //       setUsers(result.data);
  //     });
  //   }, []);
  return (
    <TicketContext.Provider value={{ type, priority, category, status }}>
      {children}
    </TicketContext.Provider>
  );
};
export default TicketContext;
