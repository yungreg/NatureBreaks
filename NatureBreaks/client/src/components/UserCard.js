import React from "react";
import { Card, CardBody, CardText, CardTitle, CardSubtitle } from "reactstrap";
import { useState, useEffect, useParams } from "react";
import VideoList from "./VideoList";

//* todo: hold and observe User State

const UserCard = ({ user }) => {
  //* todo: hold and observe User State

  return (
    <>
      <Card
        style={{
          width: "18rem",
        }}
      >
        <img alt="Sample" src={user?.profileImage} />
        <CardBody>
          <CardTitle tag="h5">{user?.firstName} </CardTitle>
          <CardSubtitle className="mb-2 text-muted" tag="h6"></CardSubtitle>
          <CardText></CardText>
        </CardBody>
      </Card>
    </>
  );
};

export default UserCard;
