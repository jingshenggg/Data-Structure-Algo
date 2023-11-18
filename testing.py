# #removing dulplicate in sorted 
# list = [1,1,2,3,4,5,5,6,8,9,10,11,11,11]
# # result = []
# # result.append(list[0])
# # # print(result)

# # def remove():
# #     temp = list[0]
# #     for i in range(1,len(list)):
# #         if temp != list[i]:
# #             temp = list[i]
# #             result.append(list[i])
# # remove()
# # print(result)

# k = []
# k.append(list[0])
# # print(k)
# temp = list[0]

# for i in range (1, len(list)):
#     if temp != list[i]:
#         temp = list[i]
#         k.append(list[i])
# print(k)


# haystack = "sadbutsad"
# needle = "sad"
# print (len(needle))
# for j in range(len(needle)):
#     print (needle[j])
# for i in range(0,len(needle)):
#     print (needle[i])

###############################################################################################
# hash table in python

# def twosum():
#     nums = [5,2,7,10,3,9]
#     target = 8
#     dic = {}
#     for i in range(len(nums)):
#         num = nums[i]
#         complement = target - num
#         if complement in dic.keys():
#             print(nums[i], complement) #print values
#             return [i, dic[complement]] #return index
#         dic[num] = i
# twosum()
    
###############################################################################################
# hash table for counting unique characters in string
from collections import defaultdict

# def find_longest_substring(s, k):
#     counts = defaultdict(int)
#     left = ans = 0
#     for right in range(len(s)):
#         counts[s[right]] += 1
#         while len(counts) > k:
#             counts[s[left]] -= 1
#             if counts[s[left]] == 0:
#                 del counts[s[left]]
#             left += 1
        
#         ans = max(ans, right - left + 1)
#     print(ans)
#     return ans
# find_longest_substring("eceba",2)

##############################################################################################
# find anagrams

from typing import List

def groupAnagrams(strs: List[str]) -> List[List[str]]:
    groups = defaultdict(list)
    for s in strs:
        key = "".join(sorted(s))
        groups[key].append(s)
    result = list(groups.values())
    print(result)
    return result

strs = ["eat", "tea", "tan", "ate", "nat", "bat"]
groupAnagrams(strs)