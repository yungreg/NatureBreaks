import React from "react";
import { Card, CardBody, CardText, CardTitle, CardSubtitle } from "reactstrap";
import { useState, useEffect, useParams } from "react";
import VideoList from "./VideoList";

//* todo: hold and observe User State

const UserCard = () => {
  //* todo: hold and observe User State

  const [user, setUser] = useState({});
  useEffect(() => {
    fetch(`https://localhost:5001/api/favoritebreaks/${user.id}`)
      .then((res) => res.json())
      .then((input) => {
        setUser(input);
      });
  }, []);

  return (
    <>
      <Card
        style={{
          width: "18rem",
        }}
      >
        <img alt="Sample" src="https://picsum.photos/300/200" />
        <CardBody>
          <CardTitle tag="h5"></CardTitle>
          <CardSubtitle className="mb-2 text-muted" tag="h6">
            {user?.firstName}
          </CardSubtitle>
          <CardText>{user.firstName}</CardText>
        </CardBody>
      </Card>
    </>
  );
};

export default UserCard;
