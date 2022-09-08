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
  const { users, type, priority, category, status } = useContext(TicketContext);

  const sendUpdatedTicket = (event) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    console.log(data.values());
    const ticket = {
      title: data.get("title"),
      description: data.get("description"),
      type: data.get("type"),
      assignee: data.get("assignee"),
      priority: data.get("priority"),
      category: data.get("category"),
      status: data.get("status"),
    };
    console.log(ticket);
    console.log("COLLECTED TICKET");
    axios.put(`${API_ENDPOINT}/api/ticket/update`, ticket);
  };
  console.log(ticket);

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

          <Select
            id="type"
            name="type"
            label="Type: "
            options={type}
            defaultValue={ticket.type}
          >
            {type.map((type) => (
              <MenuItem value={type}>{type}</MenuItem>
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
            options={users}
            defaultValue={ticket.assignees}
          >
            {users.map((user) => (
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
          <Select
            id="category"
            name="category"
            label="Category: "
            options={category}
            defaultValue={ticket.category}
          >
            {category.map((category) => (
              <MenuItem value={category}>{category}</MenuItem>
            ))}
          </Select>
          <Button variant="contained" type="submit">
            Contained
          </Button>
        </CardContent>
      </Card>
    </Box>
  );
};

export default EditTicket;
