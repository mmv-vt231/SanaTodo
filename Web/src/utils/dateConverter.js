function dateConverter(date) {
  const parsedDate = new Date(date);

  const options = {
    year: "2-digit",
    month: "2-digit",
    day: "2-digit",
    hour: "2-digit",
    minute: "2-digit",
    second: "2-digit",
  };

  return new Intl.DateTimeFormat("uk-UA", options).format(parsedDate);
}

export default dateConverter;
