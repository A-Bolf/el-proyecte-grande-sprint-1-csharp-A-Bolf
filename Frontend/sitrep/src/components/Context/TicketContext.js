import axios from "axios";
import React, { createContext, useEffect, useState } from "react";
import { API_ENDPOINT } from "../../App";

const TicketContext = createContext({});
const status = ["OPEN", "IN_PROGRESS", "RESOLVED", "CLOSED"];
const priority = ["LOW", "MEDIUM", "HIGH"];

//gonna have to replace this with a fetch all users

export const TicketProvider = ({ children }) => {
  const [Tickets, setTickets] = useState([]);
  const [assignees, setAssignees] = useState([]);
  const fetchTickets = () => {
    axios.get(`${API_ENDPOINT}/api/ticket`).then((res) => {
      setTickets(res.data);
    });
  };
  const fetchAssignees = () => {
    axios.get(`${API_ENDPOINT}/api/Auth/AllUsers`).then((response) => {
      console.log("ASSIGNEES", response.data);
      setAssignees(response.data);
    });
  };

  if (assignees.length === 0) {
    fetchAssignees();
    console.log("FIRST FETCH", assignees);
  }
  return (
    <TicketContext.Provider
      value={{
        priority,
        status,
        Tickets,
        setTickets,
        fetchTickets,
        fetchAssignees,
        assignees,
      }}
    >
      {children}
    </TicketContext.Provider>
  );
};
export default TicketContext;
