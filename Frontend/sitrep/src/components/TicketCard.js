import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Box from "@mui/material/Box";
import Chip from "@mui/material/Chip";
import Typography from "@mui/material/Typography";
import axios from "axios";
import { API_ENDPOINT } from "../App";

export default function TicketCard({
  update,
  closeModal,
  isInModal,
  onDelete,
}) {
  const onDeleteClick = () => {
    axios.delete(`${API_ENDPOINT}/api/ticket/delete/${update.id}`);
    onDelete(update);
  };
  return (
    <Card className="ticket" sx={{ minWidth: 275 }}>
      <CardContent>
        {isInModal && (
          <button
            className="btn-primary"
            style={{ float: "right", backgroundColor: "grey" }}
            type="button"
            onClick={closeModal}
          >
            x
          </button>
        )}

        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          {update.title} <Chip label={update.status} />
          <Chip label={update.type} />
        </Typography>
        <Typography variant="h5" component="div"></Typography>
        <Box component="div" sx={{ display: "block", overflow: "auto" }}>
          <Typography
            noWrap
            sx={{ mb: 1.5, display: "block", overflowY: "auto" }}
            color="text.secondary"
          >
            {update.description}
          </Typography>{" "}
        </Box>
      </CardContent>
      <CardActions>
        <Button size="small">Edit</Button>
        <Button onClick={onDeleteClick} size="small">
          Delete
        </Button>
      </CardActions>
    </Card>
  );
}
