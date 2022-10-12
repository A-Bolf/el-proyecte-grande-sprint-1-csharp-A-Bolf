import axios from "axios";
import React, { createContext, useEffect, useState } from "react";
import { API_ENDPOINT } from "../../App";

const TicketContext = createContext({});
const status = ["OPEN", "IN_PROGRESS", "RESOLVED", "CLOSED"];
const priority = ["LOW", "MEDIUM", "HIGH"];

//gonna have to replace this with a fetch all users

export const TicketProvider = ({ children }) => {
  const [Tickets, setTickets] = useState([]);
  const fetchTickets = () => {
    axios.get(`${API_ENDPOINT}/api/ticket`).then((res) => {
      setTickets(res.data);
      console.log(res.data, "RESPONSE");
    });
  };
  console.log(Tickets, "CONTEXT TICKETS");
  return (
    <TicketContext.Provider
      value={{ priority, status, Tickets, setTickets, fetchTickets }}
    >
      {children}
    </TicketContext.Provider>
  );
};
export default TicketContext;
