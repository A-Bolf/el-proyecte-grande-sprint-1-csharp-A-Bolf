import React, { useEffect, useState } from "react";
import CreateTicketButtons from "./CreateTicketButtons";
import Dropdown from "./Dropdown";
import { API_ENDPOINT } from "../App";
import { TextField } from "@mui/material";

const CreateTicket = () => {
  const handleSubmit = (e) => {
    e.preventDefault();
    const ticket = {
      title,
      description,
      priority,
      category,
      type,
      DueDate: new Date(date),
    };

    const tempURL = `${API_ENDPOINT}/api/ticket`;
    fetch(tempURL, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(ticket),
    })
      .then(() => {
        console.log(JSON.stringify(ticket), " ADDED SUCCESSFULLY");
      })
      .catch((error) => {
        console.log(error);
      });
  };
  const options = ["TASK", "BUG", "REQUEST", "OTHER"];
  const [title, setTitle] = useState("");
  const [type, setType] = useState("Task");
  const [description, setDescription] = useState("");
  const [priority, setPriority] = useState("LOW");
  const [category, setCategory] = useState("new feature");
  const [assignee, setAssignee] = useState("user1");
  const [date, setDate] = useState(new Date());
  const [ticket, setTicket] = useState({});
  useEffect(() => {
    setTicket({
      title,
      description,
      priority,
      category,
      type,
      DueDate: new Date(date),
    });
  }, [title, description, priority, category, type, date]);
  return (
    <div>
      <p style={{ fontWeight: "bold" }}>Add Issue</p>

      <div className="form-container">
        <form onSubmit={handleSubmit}>
          <CreateTicketButtons Ticket={ticket} />
          <Dropdown
            label={"Ticket Type: "}
            options={options}
            toChange={setType}
          />
          <div className="text-container">
            <TextField
              color="error"
              type="text"
              id="issue-title"
              variant="outlined"
              label="Title: "
              margin="normal"
              required
              onChange={(e) => setTitle(e.target.value)}
            />
            <br />
            <TextField
              color="error"
              type="text"
              id="issue-description"
              label="Description: "
              multiline="true"
              minRows="10"
              maxRows="15"
              margin="normal"
              required
              onChange={(e) => setDescription(e.target.value)}
            />
          </div>

          <div className="property-container">
            <p>Status: Open</p>
            <Dropdown
              label="Priority: "
              options={["low", "medium", "high"]}
              toChange={setPriority}
            />
            <Dropdown
              label="Category: "
              options={["new feature", "bugfix"]}
              toChange={setCategory}
            />
            <br />
            Date:
            <input
              type="Date"
              value={date}
              onChange={(e) => setDate(e.target.value)}
            />
            <Dropdown
              label="Assignee: "
              options={["user1", "user2"]}
              toChange={setAssignee}
            />
          </div>
          <CreateTicketButtons Ticket={ticket} />
        </form>
      </div>
    </div>
  );
};

export default CreateTicket;
