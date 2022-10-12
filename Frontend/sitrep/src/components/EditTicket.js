import React, { useContext } from "react";
import { TextField, Card } from "@mui/material";
import { CardContent } from "semantic-ui-react";
import TicketContext from "./Context/TicketContext";
import Button from "@mui/material/Button";
import axios from "axios";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import Box from "@mui/material/Box";
import { API_ENDPOINT } from "../App";

const EditTicket = ({ ticket }) => {
  const { priority, status } = useContext(TicketContext);

  const sendUpdatedTicket = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log(ticket, "Current");
    const newTicket = {
      id: ticket.id,
      title: data.get("title"),
      description: data.get("description"),
      status: data.get("status"),
      priority: data.get("priority"),
      createdDate: ticket.createdDate,
      assignees: [{ id: 2, userName: "string", passwordHash: "string" }],
    };
    console.log(newTicket, "collected");
    axios.put(`${API_ENDPOINT}/api/ticket/update`, newTicket);
  };

  return (
    <Box
      component="form"
      onSubmit={sendUpdatedTicket}
      noValidate
      sx={{ mt: 1 }}
    >
      <Card variant="outlined">
        <CardContent>
          <TextField
            name="title"
            id="title"
            label="Title"
            variant="standard"
            defaultValue={ticket.title}
          />
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
            id="assignee"
            name="assignee"
            label="Assignee: "
            options={["User1", "User2"]}
            defaultValue={ticket.assignees}
          >
            {["User1", "User2"].map((user) => (
              <MenuItem value={user}>{user}</MenuItem>
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

          <Button variant="contained" type="submit">
            Edit
          </Button>
        </CardContent>
      </Card>
    </Box>
  );
};

export default EditTicket;
