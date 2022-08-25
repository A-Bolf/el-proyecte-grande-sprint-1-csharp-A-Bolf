import Box from "@mui/material/Box";
import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Chip from "@mui/material/Chip";
import Typography from "@mui/material/Typography";

export default function TicketCard({ update, closeModal, isInModal }) {
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
        <Button size="small">Delete</Button>
      </CardActions>
    </Card>
  );
}
