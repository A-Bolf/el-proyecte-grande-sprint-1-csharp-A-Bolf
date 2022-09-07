import React from "react";
import { TextField } from "@mui/material";

const EditTicket = (ticket) => {
  console.log(ticket);
  console.log(ticket.ticket.title);
  return (
    <>
      <TextField
        id="standard-basic"
        label="Title"
        variant="standard"
        defaultValue={ticket.ticket.title}
      />
      <TextField
        id="standard-basic"
        label="Description"
        variant="standard"
        multiline="true"
        maxRows="15"
        defaultValue={ticket.ticket.description}
      />
    </>
  );
};

export default EditTicket;
