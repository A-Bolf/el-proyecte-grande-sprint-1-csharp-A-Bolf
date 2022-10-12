import Layout from "./components/Layout";
import Dashboard from "./components/DashBoard";
import CreateTicket from "./components/CreateTicket";
import { useContext, useEffect, useState } from "react";
import { Routes, Route } from "react-router-dom";
import { options } from "./components/StackedBarChart";
import LoadScreen from "./components/LoadScreen";
import axios from "axios";
import IssuesPage from "./components/IssuesPage";
import Register from "./components/Registration/Register";
import { ModalProvider } from "./components/Context/ModalContext";
import TicketContext, {
  TicketProvider,
} from "./components/Context/TicketContext";
import UserProfile from "./components/UserProfile/UserProfile";
export const API_ENDPOINT = process.env.REACT_APP_API_ENDPOINT;

const convertDate = (tickets) => {
  tickets.forEach((ticket) => {
    ticket.dueDate = new Date(ticket.dueDate).toDateString();
  });
};

function App() {
  const onTicketDelete = (ticket) => {
    setTickets(Tickets.filter((x) => x.id !== ticket.id));
  };

  function CalcTotalTicketCount(StatusData) {
    return (
      StatusData.OPEN +
      StatusData.CLOSED +
      StatusData.IN_PROGRESS +
      StatusData.RESOLVED
    );
  }
  const getMostRecentTickets = (Tickets) => {
    return Tickets.sort((a, b) => {
      return new Date(b.lastUpdatedDate) - new Date(a.lastUpdatedDate);
    }).slice(0, 3);
  };

  const getTicketStatusCounts = (Tickets) => {
    const StatusData = { OPEN: 0, CLOSED: 0, IN_PROGRESS: 0, RESOLVED: 0 };
    Tickets.forEach((ticket) => {
      if (!StatusData[ticket.status]) {
        StatusData[ticket.status] = 1;
      } else {
        StatusData[ticket.status]++;
      }
    });
    return StatusData;
  };
  const { fetchTickets, Tickets, setTickets } = useContext(TicketContext);

  const [isLoading, setIsLoading] = useState(true);
  const [StatusCounts, setStatusCounts] = useState({});
  const [Updates, setUpdates] = useState([]);

  useEffect(() => {
    setUpdates(getMostRecentTickets(Tickets));
    setStatusCounts(getTicketStatusCounts(Tickets));
  }, [Tickets]);

  useEffect(() => {
    options.scales.x.max = CalcTotalTicketCount(StatusCounts);
  }, [StatusCounts]);

  useEffect(() => {
    if (isLoading) {
      console.log("fetching");
      fetchTickets();
      axios.get(`${API_ENDPOINT}/api/ticket`).then((res) => {
        setTickets(res.data);
      });
      if (Tickets.length > 0 && StatusCounts !== {} && Updates.length > 0) {
        setIsLoading(false);
      }
    }
  }, [isLoading, Tickets.length, StatusCounts, Updates.length]);

  useEffect(() => {
    convertDate(Tickets);
  }, [Tickets]);

  if (isLoading) {
    return <LoadScreen />;
  }
  return (
    <div className="app">
      <ModalProvider>
        <Routes>
          <Route path="app/*" element={<Layout />}>
            <Route
              path="dashboard"
              element={
                <Dashboard
                  updates={Updates}
                  StatusCounts={StatusCounts}
                  onTicketDelete={onTicketDelete}
                />
              }
            />

            <Route path="add-issue" element={<CreateTicket />} />
            <Route path="issues" element={<IssuesPage tickets={Tickets} />} />
            <Route path="Profile" element={<UserProfile />} />
          </Route>
        </Routes>
      </ModalProvider>
    </div>
  );
}

export default App;
