from collections import OrderedDict

class Lot:
    def __init__(self, year, num, date, num5, win5, num4, win4, num3, win3, num2, win2, n1, n2, n3, n4, n5):
        self.year = year
        self.num = num
        self.date = date
        self.num5 = num5
        self.win5 = win5
        self.num4 = num4
        self.win4 = win4
        self.num3 = num3
        self.win3 = win3
        self.num2 = num2
        self.win2 = win2
        self.numbers = [int(n1), int(n2), int(n3), int(n4), int(n5)]
        self.sumNums = sum(self.numbers)

lots = []

file = open('otos.csv','r')

line = file.readline().strip()

while line:
    data = line.split(';')
    lots.append(Lot(data[0], data[1], data[2], data[3], data[4], data[5], data[6], data[7], data[8], 
            data[9], data[10], data[11], data[12], data[13], data[14], data[15]))
    line = file.readline().strip()

file.close()

stat = dict()

for l in lots:
    for num in l.numbers:
        if num not in stat:
            stat[num] = 1
        else:
            stat[num] = stat[num] + 1

newStatKey = OrderedDict(sorted(stat.items(), key=lambda t: t[0]))
newStatVal = OrderedDict(sorted(stat.items(), key=lambda t: t[1], reverse=True))

file = open('numbersStat.txt','w')
for n in newStatKey:
    file.write(f'{n}:{newStatKey[n]}\n')
file.close()


file = open('numbersRank.txt','w')
for n in newStatVal:
    file.write(f'{n}:{newStatVal[n]}\n')
file.close()

lots.sort(key=lambda x: x.sumNums, reverse=False)
file = open('minSumRank.txt','w')
for l in lots:
    file.write(f'{l.numbers}={l.sumNums}\n')
file.close()