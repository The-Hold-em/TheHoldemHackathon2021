const arr1 = [{ id: 1 }, { id: 2 }, { id: 3 }, { id: 4 }, { id: 5 }, { id: 6 }];
const arr2 = [{ id: 3 }, { id: 5 }, { id: 1 }, { id: 6 }, { id: 2 }, { id: 4 }];


const verify = (left = [], right = []) => {
    if (left.length != right.length) return false;
    const dif1 = left.filter(x => !right.find(o => o.id == x.id));
    const dif2 = right.filter(x => !left.find(o => o.id == x.id));
    console.log(dif1);
    console.log(dif2);
    if (dif1.length > 0 || dif2.length > 0) return false;
    return true;
}


console.log(verify(arr1, arr2));