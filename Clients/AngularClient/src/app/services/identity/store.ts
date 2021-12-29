const get = (key: string) => {
  const item: any = localStorage.getItem(key);
  return JSON.parse(item);
}
const set = (key: string, data: any) => {
  localStorage.setItem(key, JSON.stringify(data));
}
const remove = (key: string) => {
  localStorage.removeItem(key);
}
export default {
  get,
  set,
  remove
}
