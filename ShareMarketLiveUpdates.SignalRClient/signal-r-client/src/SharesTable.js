import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import ArrowDownwardIcon from '@material-ui/icons/ArrowDownward';
import ArrowUpwardIcon from '@material-ui/icons/ArrowUpward';

const useStyles = makeStyles({
  table: {
    minWidth: 350
  }
});

export default function SharesTable({shares, updatedShareId}) {
  const classes = useStyles();

  return (
    <TableContainer component={Paper}>
      <Table className={classes.table} aria-label="shares table">
        <TableHead>
          <TableRow>
            <TableCell className="font-weight-bold">Company</TableCell>
            <TableCell align="right" className="font-weight-bold">LTP</TableCell>
            <TableCell align="right" className="font-weight-bold">Bid</TableCell>
            <TableCell align="right" className="font-weight-bold">Ask</TableCell>
            <TableCell align="right" className="font-weight-bold">Last Day Closing</TableCell>
            <TableCell align="right" className="font-weight-bold">Volume</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {shares.map((share) => (
            <TableRow key={share.id} className={`animate ${share.id === updatedShareId ? "glow" : "low-opacity"}`}>
              <TableCell component="th" scope="row" className={`${share.id === updatedShareId ? "font-weight-bold" : ""}`}>
                <span className="inline-block">{share.company}</span>
                <span>
                  {share.ltp <= share.lastDayClosing ? <ArrowDownwardIcon className="color-red"/> : <ArrowUpwardIcon className="color-green"/>}
                </span>
              </TableCell>
              <TableCell className={`${share.id === updatedShareId ? "font-weight-bold" : ""}`} align="right">
                {share.ltp}
              </TableCell>
              <TableCell align="right">{share.bid}</TableCell>
              <TableCell align="right">{share.ask}</TableCell>
              <TableCell align="right">{share.lastDayClosing}</TableCell>
              <TableCell align="right">{share.volume}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
}
