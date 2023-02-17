// state of user
//iterate thru video to display ones trhat match user ID on a card
import UserCard from "./UserCard";
import Video from "./Video";
import { getAllFavorites } from "../modules/videoManager";
import { useEffect, useState } from "react";

const FavoriteBreaks = ({ user }) => {
  const [faveVideos, setFaveVideos] = useState([]);

  const favoriteVideos = () => {
    getAllFavorites().then((data) => setFaveVideos(data));
  };
  useEffect(() => {
    favoriteVideos();
  }, []);

  return (
    <>
      <UserCard user={user} />
      <div className="row justify-content-center">
        {faveVideos.map((video) => (
          <Video user={user} video={video?.video} key={video?.video?.id} />
        ))}
      </div>
    </>
  );
};

export default FavoriteBreaks;
