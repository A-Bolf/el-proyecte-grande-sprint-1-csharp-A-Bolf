import React, { useContext } from "react";
import { TextField, Card } from "@mui/material";
import { CardContent } from "semantic-ui-react";
import TicketContext from "./Context/TicketContext";
import Button from "@mui/material/Button";
import axios from "axios";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import Box from "@mui/material/Box";
import ModalContext from "./Context/ModalContext";
import { API_ENDPOINT } from "../App";

const EditTicket = ({ ticket }) => {
  console.log(ticket, "THIS IS THE TICKET IN QUESTION");
  const { priority, status, setTickets, Tickets, fetchAssignees, assignees } =
    useContext(TicketContext);
  const { toggleModal } = useContext(ModalContext);
  const sendUpdatedTicket = (event) => {
    event.preventDefault();

    const data = new FormData(event.currentTarget);
    const getUserById = (id) => {
      let ret = assignees.filter((x) => {
        return x.id == id;
      });
      return ret;
    };
    const newTicket = {
      id: ticket.id,
      assignees: getUserById(data.get("assignee")),
      title: data.get("title"),
      description: data.get("description"),
      status: data.get("status"),
      priority: data.get("priority"),
    };

    axios
      .put(`${API_ENDPOINT}/api/ticket/update`, newTicket)
      .then((response) => {
        let ticket = response.data;
        var foundIndex = Tickets.findIndex((x) => x.id === ticket.id);
        Tickets[foundIndex] = ticket;
        setTickets(
          Tickets.map((x) => {
            if (x.id === ticket.id) {
              return ticket;
            }
            return x;
          })
        );
        toggleModal();
      });
  };

  return (
    <Box
      component="form"
      onSubmit={sendUpdatedTicket}
      noValidate
      sx={{ border: 1 }}
    >
      <Card variant="outlined">
        <CardContent>
          <div className="editTicketItemContainer">
            <TextField
              name="title"
              id="title"
              label="Title"
              variant="standard"
              defaultValue={ticket.title}
            />

            <br />
            <TextField
              name="description"
              id="description"
              label="Description"
              variant="standard"
              multiline="true"
              maxRows="15"
              defaultValue={ticket.description}
            />
            <br />
            <Select
              id="status"
              name="status"
              label="Status: "
              options={status}
              defaultValue={ticket.status}
            >
              {status.map((stat) => (
                <MenuItem value={stat}>{stat}</MenuItem>
              ))}
            </Select>
            <Select
              id="assignee"
              name="assignee"
              label="Assignee: "
              options={assignees}
              defaultValue={1}
            >
              {assignees.map((user) => (
                <MenuItem value={user.id}>{user.userName}</MenuItem>
              ))}
            </Select>
            <Select
              id="priority"
              name="priority"
              label="Priority: "
              options={priority}
              defaultValue={ticket.priority}
            >
              {priority.map((priority) => (
                <MenuItem value={priority}>{priority}</MenuItem>
              ))}
            </Select>

            <Button
              variant="contained"
              type="submit"
              sx={{ bottom: "25%", right: "10%", position: "absolute" }}
            >
              Edit
            </Button>
          </div>
        </CardContent>
      </Card>
    </Box>
  );
};

export default EditTicket;
