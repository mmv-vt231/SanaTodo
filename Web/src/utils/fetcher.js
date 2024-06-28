export const fetcher = async body => {
  try {
    const response = await fetch("https://localhost:7056/graphql", {
      method: "POST",
      headers: { 
        "Content-Type": "application/json",
        "storage": localStorage.getItem("storage") ?? "db"
      },
      body: JSON.stringify(body),
    });

    const { data } = await response.json();
    return data;
  } catch (err) {
    console.log(err.message);
  }

  return {};
};
